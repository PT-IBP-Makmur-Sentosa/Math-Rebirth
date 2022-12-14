using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private UIInventoryPage shopUI;

        [SerializeField]
        private InventorySO shopData;

        private InventorySaveSystem inventorySaveSystem;

        private bool inShopArea = false;
        private void Awake()
        {
            PrepareUI();
            PrepareShopUI();
            PrepareInventoryData();
            PrepareShopData();
        }

        private void PrepareInventoryData()
        {
            inventorySaveSystem = gameObject.GetComponent<InventorySaveSystem>();
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            if (inventorySaveSystem.InventorySaveExists())
            {
                List<InventoryItem> saveditems = inventorySaveSystem.LoadInventorySave();
                foreach (InventoryItem item in saveditems)
                {
                    if (item.IsEmpty)
                        continue;
                    inventoryData.AddItem(item);
                }
            }
            // foreach (InventoryItem item in initialItems)
            // {
            //     if (item.IsEmpty)
            //         continue;
            //     inventoryData.AddItem(item);
            // }

        }
        private void PrepareShopData()
        {
            shopData.Initialize();
            shopData.OnInventoryUpdated += UpdateShopUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                shopData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void UpdateShopUI(Dictionary<int, InventoryItem> inventoryState)
        {
            shopUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                shopUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void PrepareShopUI()
        {
            shopUI.InitializeInventoryUI(shopData.Size);
            shopUI.OnDescriptionRequested += HandleShopDescriptionRequest;
            shopUI.OnItemActionRequested += HandleShopItemActionRequest;
        }
        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }

        }

        private void HandleShopItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = shopData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                shopUI.ShowItemAction(itemIndex);
                InventoryItem tempInventoryItem = new InventoryItem
                {
                    item = inventoryItem.item,
                    quantity = 1,
                };
                shopUI.AddAction("Buy", () => BuyItem(tempInventoryItem));
            }

        }
        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }

        private void BuyItem(InventoryItem inventoryItem)
        {
            inventoryData.AddItem(inventoryItem);
            shopUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }
        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;

            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);
        }

        private void HandleShopDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = shopData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;

            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            shopUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                    $": {inventoryItem.itemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameObject glob = GameObject.Find("GlobalObject");
                if (!glob.GetComponent<GlobalControl>().inCombat && !shopUI.isActiveAndEnabled)
                {
                    if (inventoryUI.isActiveAndEnabled == false)
                    {
                        inventoryUI.Show();
                        foreach (var item in inventoryData.GetCurrentInventoryState())
                        {
                            inventoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                        }

                    }
                    else
                    {
                        inventoryUI.Hide();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject glob = GameObject.Find("GlobalObject");
                if (!glob.GetComponent<GlobalControl>().inCombat && inShopArea && !inventoryUI.isActiveAndEnabled)
                {
                    if (shopUI.isActiveAndEnabled == false)
                    {
                        shopUI.Show();
                        foreach (var item in shopData.GetCurrentInventoryState())
                        {
                            shopUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                        }

                    }
                    else
                    {
                        shopUI.Hide();
                    }
                }

            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Shop")
                inShopArea = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Shop")
                inShopArea = false;
        }
    }
}