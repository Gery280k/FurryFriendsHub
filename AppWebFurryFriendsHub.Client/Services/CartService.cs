using AppWebFurryFriendsHub.Shared;

namespace AppWebFurryFriendsHub.Client.Services
{
    public class CartService
    {
        public event Action OnChange;

        public List<CartItem> items = new();
        public bool ShowAlert { get; private set; } // Estado de la alerta
        public bool IsCartVisible { get; private set; } // Estado de la visibilidad del carrito

        public IReadOnlyList<CartItem> Items => items;

        // Método para agregar un producto al carrito

        public void AddToCart(Product product)
        {
            var existingItem = Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                items.Add(new CartItem { Product = product, Quantity = 1 });
            }

            ShowAlertMessage();
            ToggleCartVisibility(); // Abre el carrito
            NotifyStateChanged();
        }

        public async Task ShowAlertMessage()
        {
            ShowAlert = true;
            NotifyStateChanged(); // Notifica el cambio de estado

            await Task.Delay(3000); // Espera 3 segundos

            CloseAlert(); // Llama a CloseAlert para cerrar la alerta
            NotifyStateChanged(); // Notifica el cambio después de cerrar la alerta
        }

        public void ToggleCartVisibility()
        {
            IsCartVisible = true;
            NotifyStateChanged();
        }


        // Método para quitar un producto del carrito
        public void RemoveFromCart(string productId)
        {
            var item = items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)
            {
                items.Remove(item);
            }
            NotifyStateChanged(); // Notificar que el estado cambió
        }

        // Método para actualizar la cantidad de un producto
        public void UpdateQuantity(string productId, int quantity)
        {
            var item = items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    items.Remove(item); // Eliminar el producto si la cantidad es 0 o menos
                }
                else
                {
                    item.Quantity = quantity; // Actualizar la cantidad
                }
                NotifyStateChanged(); // Notificar que el estado cambió
            }
        }

        public void CloseAlert()
        {
            ShowAlert = false;
            NotifyStateChanged(); // Notifica que el estado cambió
        }
        public void ClearCart()
        {
            items.Clear(); // Vacía la lista de artículos
            NotifyStateChanged(); // Notifica el cambio
        }


        private void NotifyStateChanged() => OnChange?.Invoke();


    }
}
