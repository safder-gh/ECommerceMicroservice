using IMS.CoreBusiness;
using IMS.UseCase.PluginInterfaces;

namespace IMS.Plugins.InMemory
    {
    public class InventoryRepository : IInventoryRepository
        {
        private List<Inventory> Inventories;
        public InventoryRepository()
            {
            this.Inventories = GetInventories();
            }
        public async Task<IEnumerable<Inventory>> GetByNameAsync(string name)
            {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(Inventories);
            return Inventories.Where(i=>i.Name.Contains(name,StringComparison.OrdinalIgnoreCase));
            }
        private   List<Inventory> GetInventories()
            {
            var random = new Random();

            string[] productNames =
            {
            "Laptop", "Mouse", "Keyboard", "Monitor", "Printer",
            "Desk", "Chair", "Tablet", "Phone", "Speaker",
            "Headphones", "Webcam", "Microphone", "USB Drive", "SSD",
            "Hard Drive", "Graphics Card", "CPU", "RAM", "Motherboard",
            "Power Bank", "Router", "Switch", "Projector", "Scanner",
            "Smart Watch", "Camera", "Tripod", "Drone", "TV",
            "Gaming Console", "Game Controller", "VR Headset", "Fan", "Air Conditioner",
            "Refrigerator", "Microwave", "Coffee Maker", "Blender", "Toaster",
            "Vacuum Cleaner", "Iron", "Washing Machine", "Dryer", "Water Bottle",
            "Backpack", "Notebook", "Pen", "Pencil", "Marker",
            "Calculator", "Desk Lamp", "Bookshelf", "Clock", "Whiteboard",
            "Extension Cable", "HDMI Cable", "Ethernet Cable", "Charger", "Battery",
            "Flashlight", "Tool Kit", "Hammer", "Drill", "Screwdriver",
            "Wrench", "Pliers", "Measuring Tape", "Ladder", "Helmet",
            "Safety Glasses", "Gloves", "Paint Brush", "Paint Roller", "Bucket",
            "Garden Hose", "Lawn Mower", "Plant Pot", "Seeds", "Fertilizer",
            "T-Shirt", "Jeans", "Jacket", "Shoes", "Cap",
            "Socks", "Belt", "Wallet", "Sunglasses", "Watch",
            "Perfume", "Soap", "Shampoo", "Conditioner", "Toothbrush",
            "Toothpaste", "Towel", "Pillow", "Blanket", "Curtains"
        };

            var products = new List<Inventory>();

            for (int i = 0; i < 100; i++)
                {
                products.Add(new Inventory
                    {
                    Id = Guid.CreateVersion7(),
                    Name = $"{productNames[i]} {(i + 1)}",
                    Quantity = random.Next(1, 501),           // 1 - 500
                    Price = Math.Round(
                        (decimal)(random.NextDouble() * 490 + 10),
                        2)                                    // $10.00 - $500.00
                    });
                }

            return products;
            }
        }
    }
