using UnityEngine;
using ThunderRoad;

//Namespace of your library project
namespace LioScripts
{
    class MapManager : MonoBehaviour
    {
        //Parent gameobject of all "Enemy Spawners" that will be respawned
        [SerializeField]
        GameObject MobsRoot;
        //The Zone gameobject at the start of the level is disabled at first and enabled when the player grabbed the rune
        [SerializeField]
        GameObject LevelEndZone;
        //Component used to laod the Home level
        [SerializeField]
        EventLoadLevel levelLoader;
        //Component used to display messages
        [SerializeField]
        EventMessage msgEvent;

        bool hasRespawned;

        string msgStart = "Get back here after you found the artifact!";
        string msgLoot = "You got the rune! Time to get out..";
        string msgEnd = "Well done ! Going back home..";

        //Called at the start of the game
        private void Start()
        {
            hasRespawned = false;
            ShowMessage(msgStart);
        }

        //Called when the player grabbed the rune
        public void RespawnAllMobs()
        {
            if (!hasRespawned)
            {
                hasRespawned = true;
                //Despawn the remaining enemies
                foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>())
                    if (g.activeInHierarchy && g.GetComponent<Creature>() != null && !g.name.Contains("Player"))
                        g.GetComponent<Creature>().Despawn();

                //Then respawn them
                for (int i = 0; i < MobsRoot.transform.childCount; i++)
                    MobsRoot.transform.GetChild(i).gameObject.GetComponent<CreatureSpawner>().Spawn();

                //Enable end zone at the start
                LevelEndZone.SetActive(true);
                ShowMessage(msgLoot);
            }
        }

        //Called by the zone at the start
        public void EndGame()
        {
            ShowMessage(msgEnd);
            levelLoader.LoadLevel("Home");
        }

        void ShowMessage(string msg)
        {
            msgEvent.text = msg;
            msgEvent.ShowMessage();
        }
    }
}
