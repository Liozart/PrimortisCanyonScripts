using ThunderRoad;
using UnityEngine;

namespace LioScripts
{
    public class DragonicFireRuneItem : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);

            //item.gameObject.AddComponent<DragonicFireRune>();
        }

    }


   public class DragonicFireRune : MonoBehaviour
    {
        MapManager mapManager;
        bool firstGrab = false;
        float fireDelay = 2f;
        Side sideHandledWhileCast;

        protected void Awake()
        {
            GetComponent<Item>().OnHeldActionEvent += Item_OnHeldActionEvent;
            mapManager = GameObject.Find("Map").GetComponent<MapManager>();
        }

        private void OnDestroy() => this.item.OnHeldActionEvent -= new Item.HeldActionDelegate(this.Item_OnHeldActionEvent);

        public void Update()
        {
            if (fireDelay > 0f)
                fireDelay -= Time.deltaTime;
        }

        private void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if (!firstGrab)
            {
                firstGrab = true;
                mapManager.RespawnAllMobs();
            }
            if (fireDelay <= 0f &&action == Interactable.Action.UseStart)
            {
                fireDelay = 0.5f;
                sideHandledWhileCast = ragdollHand.side;
                Catalog.GetData<ItemData>("SpellFire").SpawnAsync(new System.Action<Item>(this.FireTheBall));
            }
        }

        void FireTheBall(Item fireball)
        {
            fireball.transform.position = gameObject.transform.position;
            fireball.transform.rotation = gameObject.transform.rotation;
            //fireball.gameObject.GetComponent<Rigidbody>().AddForce(-fireball.transform.forward * (this.bulletForce * bullet.rb.mass), ForceMode.Impulse);
            
            fireball.Throw();
        }
    }
}
