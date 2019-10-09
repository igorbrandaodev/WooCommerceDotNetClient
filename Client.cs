using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;
using WooCommerceNET.WooCommerce.v3.Extension;

namespace WooCommerceDotNetClient
{
    public class Client
    {
        static async Task Main(string[] args)
        {

            // Calling the endpoints directly

            /*
            string url, key, secret;
            url = "http://111.1.11.111/your-store/wp-json/wc/v3/";
            key = "";
            secret = "";
            RestAPI rest = new RestAPI(url, key, secret);
            WCObject wc = new WCObject(rest);

            var pedidos = await wc.Order.GetAll(new Dictionary<string, string>() {
                { "per_page", "100" } });

            var clientes = await wc.Customer.GetAll(new Dictionary<string, string>() {
                { "per_page", "100" } });

            foreach (var pedido in pedidos)
            {
                WooCommerceNET.WooCommerce.v2.OrderMeta metadados = pedido.meta_data.SingleOrDefault(x => x.key == "_billing_cpf");
                string cpf = metadados.value.ToString();

            }

            var payment = await wc.PaymentGateway.GetAll();


            // Update PRODUTO
            //await wc.Product.Update(5055, new Product { stock_quantity = 666 });

            Console.ReadLine();

            */


            // Calling the endpoints via methods

            List<Product> list = await GetProducts();

            //string sku = "0001#SEP#0700200024#SEP#U,0001#SEP#0700300011#SEP#U";
            //List<Product> list = await GetProducts(sku);

            /*
            Product p = new Product()
            {
                name = "Produto teste 2",
                price = 8.0M
            };

            await AddProduct(p);
            */

            //Product p = await GetProductBySku("0001#SEP#0600101350#SEP#U");

            /*
            int id = 5089;
            Product p = await GetProductById(id.ToString());

            p.id = 6000;
            p.name = "teste edição";
            await UpdateProduct(6000, p);
            */

        }

        public static WCObject ApiClient()
        {
            string url, key, secret;

            url = "http://111.1.11.111/your-store/wp-json/wc/v3/";
            key = "";
            secret = "";

            try
            {
                // Instance the API client
                RestAPI rest = new RestAPI(url, key, secret);
                WCObject wc = new WCObject(rest);

                return wc;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        // Get PRODUCTS
        public static async Task<List<Product>> GetProducts()
        {
            try
            {
                WCObject wc = ApiClient();

                var p = await wc.Product.GetAll(new Dictionary<string, string>() {
                { "per_page", "100" } });

                return p;
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        // Get PRODUCT by SKU (can separate with comma: "sku,sku...)
        public static async Task<List<Product>> GetProducts(string sku)
        {
            try
            {
                WCObject wc = ApiClient();

                var p = await wc.Product.GetAll(new Dictionary<string, string>() {
                { "sku",  sku} });

                if (p != null)
                {
                    return p;
                }

                return null;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        // Get ORDER by Id
        public static async Task<Product> GetProductById(string id)
        {
            try
            {
                WCObject wc = ApiClient();

                var p = await wc.Product.GetAll(new Dictionary<string, string>() {
                { "id",  id} });

                if (p.Count > 0)
                {
                    return p[0];
                }

                return null;
            }
            catch (Exception e)
            {

                throw e;
            }


        }


        

        // Create PRODUCT
        public static async Task<Product> AddProduct(Product product)
        {
            try
            {
                WCObject wc = ApiClient();

                return await wc.Product.Add(product);
            }
            catch (Exception e)
            {

                throw e;
            }


        }


        // Update PRODUCT
        public static async Task<bool> UpdateProduct(int id, Product product)
        {

            try
            {
                WCObject wc = ApiClient();

                var p = await wc.Product.Update(id, product);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;

            }

        }

        // Update STOCK
        public static async Task<Product> UpdateStock(int id, int stock)
        {
            try
            {
                WCObject wc = ApiClient();

                return await wc.Product.Update(id, new Product { stock_quantity = stock });
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        // Get ORDERS created after a date
        public static async Task<List<Order>> GetOrders(string date)
        {
            try
            {
                WCObject wc = ApiClient();

                var orders = await wc.Order.GetAll(new Dictionary<string, string>() {
                { "after", date } });

                return orders;
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        // Get ORDERS by Id
        public static async Task<Order> GetOrderById(string id)
        {
            try
            {
                WCObject wc = ApiClient();

                var orders = await wc.Order.GetAll(new Dictionary<string, string>() {
                { "id",  id} });

                if (orders.Count > 0)
                {
                    return orders[0];
                }

                return null;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        // Get CLIENT by Id
        public static async Task<Customer> GetCustomerById(string id)
        {
            try
            {
                WCObject wc = ApiClient();

                var customer = await wc.Customer.Get(Convert.ToInt32(id));  //.GetAll(new Dictionary<string, string>() {{ "id",  id} });

                if (customer != null)
                {
                    return customer;
                }

                return null;
            }
            catch (Exception e)
            {

                throw e;
            }


        }


    }
}
