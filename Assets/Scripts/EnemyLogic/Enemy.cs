using System;
using Descriptors;
using Inventory;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        private ItemDescriptor _itemDescriptor;
        
        public float CurrentHp { get; private set; }
        
        public EnemyDescriptor EnemyDescriptor { get; private set; }
        
        public event Action OnDamageReceived;

        public void Init(EnemyDescriptor enemyDescriptor, ItemDescriptor itemDescriptor)
        {
            CurrentHp = enemyDescriptor.Hp;
            EnemyDescriptor = enemyDescriptor;
            _itemDescriptor = itemDescriptor;
        }

        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;
            OnDamageReceived?.Invoke();
            
            if (CurrentHp <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            DropItem();
            Destroy(gameObject);
        }

        private void DropItem()
        {
            GameObject lootableObject = new("LootableItem");
    
            lootableObject.transform.position = transform.position;
            
            lootableObject.AddComponent<Rigidbody2D>();
            
            SpriteRenderer spriteRenderer = lootableObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _itemDescriptor.Icon;
            Vector2 spriteSize = spriteRenderer.bounds.size;
            
            BoxCollider2D itemCollider = lootableObject.AddComponent<BoxCollider2D>();
            itemCollider.size = spriteSize;
            itemCollider.isTrigger = true;

            GameItem gameItem = lootableObject.AddComponent<GameItem>();
            gameItem.GameItemInfo = new GameItemInfo(_itemDescriptor.Id, _itemDescriptor.ItemName,
                _itemDescriptor.Quantity, _itemDescriptor.Icon);
        }
    }
}