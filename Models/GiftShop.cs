//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using $safeprojectname$.Models;

//namespace $safeprojectname$.Models
//{
//    public class GiftShop
//    {
//        PetaByteLiveContext db = new PetaByteLiveContext();

//        /*
//         * Note(bryanstephens): Since a user will remain anonymous will adding, removing and paying for items in their cart, need to ensure cartId carries over from page to page via session variable
//         */
//        public string CartId { get; set; }

//        public const string CartSessionId = "CartId";

//        // HttpContext ==> encapsualtes speific information about individual HTTP requests

//        public static GiftShop GetCart(HttpContextBase context)
//        {
//            var cart = new GiftShop();
//            cart.CartId = cart.GetCartId(context);
//            return cart;
//        }
//        public static GiftShop GetCart(Controller controller)
//        {
//            return GetCart(controller.HttpContext);
//        }
//        // add an item to the count; if the user is adding an item a second time, increase its count in the cart
//        public void AddToCart(giftItem giftitem)
//        {
//            var item = db.carts.SingleOrDefault(c => c.cartSessionId == CartSessionId && c.cartId == giftitem.giftItemId);

//            // if item does exist in cart, add it
//            if (item == null)
//            {
//                item = new cart
//                {
//                    cartSessionId = CartSessionId,
//                    count = 1,
//                    dateCreated = DateTime.Now,
//                    GiftId = giftitem.giftItemId
//                };
//                db.carts.Add(item);
//            }
//            // if it does, increase it's count
//            else
//            {
//                item.count++;
//            }
//            db.SaveChanges();
//        }

//        // get the total price of gift items in gift cart
//        public decimal GetTotal()
//        {
//            decimal? total = (from cartItems in db.carts
//                              where cartItems.cartSessionId == CartSessionId
//                              select (int?)cartItems.count * cartItems.giftItem.giftItemPrice).Sum();

//            return total ?? decimal.Zero;
//        }

//        // Users can remove items from cart
//        public int RemoveFromCart(int id)
//        {
//            var item = db.carts.SingleOrDefault(c => c.cartSessionId == CartSessionId && c.cartId == id);

//            int itemCount = 0;

//            if (item != null)
//            {
//                if (item.count > 1)
//                {
//                    item.count--;
//                    itemCount = item.count;
//                }
//                else
//                {
//                    db.carts.Remove(item);
//                }

//                db.SaveChanges();
//            }

//            return itemCount;
//        }

//        // allow the user to empty the cart (if any items are in it)
//        public void EmptyCart()
//        {
//            var items = db.carts.Where(c => c.cartSessionId == CartSessionId);

//            foreach (var item in items)
//            {
//                db.carts.Remove(item);
//            }
//            db.SaveChanges();
//        }

//        // access the items the cart (for the view, allows the ability to display all of the items 
//        public List<cart> GetCartItems()
//        {
//            return db.carts.Where(c => c.cartSessionId == CartSessionId).ToList();
//        }

//        // 
//        public int GetCount()
//        {
//            int? count = (from cartItems in db.carts
//                          where cartItems.cartSessionId == CartSessionId
//                          select (int?)cartItems.count).Sum();

//            return count ?? 0;
//        }

//        // public users can add item(s) to the cart; for each item in the cart, the cart will display the count (occurances of a single item) and amount (the total price of an item * by the price of the item)
//        public int CreateOrder(orderPlaced order)
//        {
//            // initialize orderTotal at 0 (since no items are added by default)
//            decimal orderTotal = 0;

//            // variable to hold new insance of GetCartItems()
//            var cartItems = GetCartItems();

//            // for each item in cartItems...
//            foreach (var item in cartItems)
//            {
//                // ... grab the quantity of the item, it's id, and the id of the order placedd
//                var ordered = new orderPlaced
//                {
//                    productId = item.GiftId,
//                    orderId = order.orderId,
//                    quantity = item.count
//                };

//                // orderTotal = the quantity of a single item * the price of of an individual quantity of the item
//                orderTotal += (item.count * item.giftItem.giftItemPrice);

//                db.orderPlaceds.Add(ordered);

//            }
//            // return the total cost of all items in the art
//            order.CustomerOrder.Amount = orderTotal;

//            db.SaveChanges();

//            // once a customer is done with the transaction, empty their cart
//            EmptyCart();

//            return order.orderId;
//        }

//        public string GetCartId(HttpContextBase context)
//        {
//            if (context.Session[CartSessionId] == null)
//            {
//                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
//                {
//                    context.Session[CartSessionId] = context.User.Identity.Name;
//                }
//                else
//                {
//                    // for each order, create a unique id for cart id
//                    Guid tempCartId = Guid.NewGuid();
//                    context.Session[CartSessionId] = tempCartId.ToString();
//                }
//            }
//            return context.Session[CartSessionId].ToString();
//        }


//    }
//}