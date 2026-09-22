using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Services;

public class CoffeeCatalogService : ICoffeeCatalogService
{
    private readonly object _lock = new();
    private readonly List<CoffeeProduct> _products = new()
    {
        // === BURGERS & SNACKS ===
        new CoffeeProduct
        {
            Id = 1,
            Name = "Giant JMT 10-Inch Burger",
            Description = "Our colossal 10-inch party burger loaded with flame-grilled pure beef patties, crispy bacon strips, fresh tomatoes, crunchy pipino slices, lettuce, and melted cheese, served with a heap of golden French fries.",
            Price = 879.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_giant_10inch_burger_real.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 120,
            StockQuantity = 20,
            Origin = "JMT Cafe Dalaguete Legend",
            TastingNotes = new() { "10-Inch Giant Bun", "Beef Patty", "Bacon", "Pipino & Fries" },
            AvailableSizes = new() { "10-Inch Giant (Serves 4-6)" },
            AvailableTemperatures = new(),
            Ingredients = "Massive 10-inch toasted bun, seasoned beef patties, bacon, pipino, tomatoes, lettuce, cheese, secret JMT dressing, fries.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Ultimate celebration feast for barkadas, road-trippers, and families visiting Dalaguete!"
        },
        new CoffeeProduct
        {
            Id = 2,
            Name = "Combo Burger (2 Burgers, Fries & Drink)",
            Description = "The popular JMT value combo: 2 freshly grilled juicy beef burgers with French fries, sliced tomatoes, fresh pipino, lettuce, melted cheese, and 1 refreshing drink.",
            Price = 185.00m,
            Category = "Value Feast Combos",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_combo_burger_set_real.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 95,
            StockQuantity = 45,
            Origin = "Best-Selling JMT Combo",
            TastingNotes = new() { "2 Burgers", "Seasoned Fries", "Chilled Drink", "Crisp Veggies" },
            AvailableSizes = new() { "2 Burgers + 1 Fries + 1 Drink" },
            AvailableTemperatures = new(),
            Ingredients = "2 beef burgers, French fries, fresh tomato, pipino, lettuce, cheese slice, chilled beverage.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Satisfying meal for 2 people or a very hungry explorer."
        },
        new CoffeeProduct
        {
            Id = 3,
            Name = "American Burger",
            Description = "Classic hearty American-style beef burger with melted cheese, crispy lettuce, ripe tomatoes, pickles, and signature house burger sauce.",
            Price = 129.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_american_burger_deluxe.png",
            IsFeatured = true,
            Rating = 4.9,
            ReviewCount = 68,
            StockQuantity = 40,
            Origin = "JMT Signature Grill",
            TastingNotes = new() { "Thick Beef Patty", "American Cheese", "Tangy Sauce" },
            AvailableSizes = new() { "Single Patty", "Double Patty (+₱45)" },
            AvailableTemperatures = new(),
            Ingredients = "All-beef patty, cheddar cheese, lettuce, tomato, pickles, mayo, ketchup, mustard, toasted bun.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Pairs deliciously with an iced Spanish latte or fresh mango shake."
        },
        new CoffeeProduct
        {
            Id = 4,
            Name = "Chicken Strips Burger with Fries",
            Description = "Crispy golden fried chicken breast strips smothered in creamy cheese sauce and fresh crisp lettuce on a soft toasted bun, served with seasoned fries.",
            Price = 85.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_chicken_strips_burger.png",
            IsFeatured = true,
            Rating = 4.9,
            ReviewCount = 74,
            StockQuantity = 50,
            Origin = "JMT Chicken Specialty",
            TastingNotes = new() { "Crunchy Chicken Strips", "Rich Cheese Sauce", "Golden Fries" },
            AvailableSizes = new() { "Burger + Fries" },
            AvailableTemperatures = new(),
            Ingredients = "Crispy battered chicken fillet strips, cheddar cheese sauce, lettuce, toasted bun, French fries.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Customer favorite snack along the Tabon road."
        },
        new CoffeeProduct
        {
            Id = 5,
            Name = "Bacon Burger",
            Description = "Savory beef patty crowned with crispy smoked bacon strips, melted cheese, ripe tomato, and fresh crisp greens on a sesame seed bun.",
            Price = 85.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_bacon_burger.png",
            IsFeatured = true,
            Rating = 4.85,
            ReviewCount = 58,
            StockQuantity = 40,
            Origin = "Smoked Bacon Classic",
            TastingNotes = new() { "Tomato", "Pure Beef", "Crispy Bacon", "Lettuce" },
            AvailableSizes = new() { "Solo Burger" },
            AvailableTemperatures = new(),
            Ingredients = "Beef patty, smoked bacon, cheddar cheese, lettuce, tomato, toasted sesame bun.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Cooked fresh to order on the grill. Also available as a Combo with Fries & Drink!"
        },
        new CoffeeProduct
        {
            Id = 6,
            Name = "Chicken Fillet Burger",
            Description = "Crispy and juicy seasoned chicken fillet on a toasted bun with melted cheese, crisp lettuce, and savory house dressing.",
            Price = 75.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_chicken_fillet_burger.png",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 42,
            StockQuantity = 35,
            Origin = "Budget Friendly Chicken",
            TastingNotes = new() { "Chicken Fillet", "Melted Cheese", "Crisp Lettuce" },
            AvailableSizes = new() { "Solo Burger" },
            AvailableTemperatures = new(),
            Ingredients = "Seasoned chicken fillet, melted cheese, lettuce, house dressing, toasted bun.",
            Allergens = "Wheat, Eggs, Dairy",
            BrewGuide = "Delicious crunchy chicken snack in Tabon, Dalaguete."
        },
        new CoffeeProduct
        {
            Id = 7,
            Name = "BUY 1 TAKE 1 NORMAL BURGER",
            Description = "Unbeatable promo value! Two freshly grilled beef burgers on warm toasted buns with sliced cheese and signature savory dressing.",
            Price = 65.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_buy1take1_burger_real.png",
            IsFeatured = true,
            Rating = 4.95,
            ReviewCount = 110,
            StockQuantity = 60,
            Origin = "Dalaguete Best Value Promo",
            TastingNotes = new() { "2 Burgers Pack", "Beef Patty", "Sliced Cheese" },
            AvailableSizes = new() { "2 Burgers Promo Pack" },
            AvailableTemperatures = new(),
            Ingredients = "2 beef patties, 2 toasted buns, sliced cheese, special burger sauce.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Best-selling buy 1 take 1 special for friends and travelers."
        },
        new CoffeeProduct
        {
            Id = 8,
            Name = "Classic Burger",
            Description = "Comforting, tasty grilled beef patty on a soft bun with cheddar cheese and fresh crisp lettuce.",
            Price = 65.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_classic_burger.png",
            IsFeatured = false,
            Rating = 4.75,
            ReviewCount = 39,
            StockQuantity = 45,
            Origin = "Neighborhood Classic",
            TastingNotes = new() { "Pure Beef", "Cheddar Cheese", "Crisp Lettuce" },
            AvailableSizes = new() { "Solo Burger" },
            AvailableTemperatures = new(),
            Ingredients = "Beef patty, cheddar cheese, crisp lettuce, house dressing, bun.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Classic daily favorite at JMT Cafe."
        },
        new CoffeeProduct
        {
            Id = 9,
            Name = "Footlong Bread Cheese",
            Description = "Long toasted footlong bread stuffed with savory sausage and smothered in melted cheese.",
            Price = 45.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_footlong_bread_cheese_real.png",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 48,
            StockQuantity = 30,
            Origin = "Snack Counter Favorite",
            TastingNotes = new() { "Cheesy Goodness", "Footlong Sausage", "Toasted Bread" },
            AvailableSizes = new() { "Footlong (12 inches)" },
            AvailableTemperatures = new(),
            Ingredients = "Footlong roll, footlong hotdog, melted cheese sauce, condiments.",
            Allergens = "Wheat, Dairy",
            BrewGuide = "Served hot and toasty."
        },
        new CoffeeProduct
        {
            Id = 10,
            Name = "Crispy French Fries",
            Description = "Golden, crunchy shoestring French fries lightly seasoned with savory cheese seasoning and served hot with dipping sauce.",
            Price = 35.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_french_fries_cheese_real.png",
            IsFeatured = false,
            Rating = 4.85,
            ReviewCount = 82,
            StockQuantity = 70,
            Origin = "Crispy Sides",
            TastingNotes = new() { "Crunchy Potato", "Savory Salt", "Golden Fried" },
            AvailableSizes = new() { "Regular Basket" },
            AvailableTemperatures = new(),
            Ingredients = "Potatoes, vegetable oil, seasoning salt, cheese powder.",
            Allergens = "None",
            BrewGuide = "Best sidekick to any burger or cold drink."
        },
        new CoffeeProduct
        {
            Id = 11,
            Name = "Pan Cake",
            Description = "Fluffy, warm pan cake griddled golden brown, served with butter and sweet syrup.",
            Price = 30.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "",
            IsFeatured = false,
            Rating = 4.7,
            ReviewCount = 28,
            StockQuantity = 30,
            Origin = "Breakfast & Merienda",
            TastingNotes = new() { "Fluffy Batter", "Golden Crust", "Sweet Syrup" },
            AvailableSizes = new() { "1 Piece", "Stack of 2 (+₱25)" },
            AvailableTemperatures = new(),
            Ingredients = "Flour, eggs, milk, sugar, butter, maple flavored syrup.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Great with hot morning coffee."
        },
        new CoffeeProduct
        {
            Id = 12,
            Name = "Local Bread",
            Description = "Freshly baked local Dalaguete bakery bread, warm and comforting.",
            Price = 25.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "",
            IsFeatured = false,
            Rating = 4.6,
            ReviewCount = 21,
            StockQuantity = 40,
            Origin = "Local Dalaguete Bake",
            TastingNotes = new() { "Soft Crumb", "Gentle Sweetness" },
            AvailableSizes = new() { "Serving" },
            AvailableTemperatures = new(),
            Ingredients = "Flour, yeast, sugar, salt, milk.",
            Allergens = "Wheat",
            BrewGuide = "Simple and traditional."
        },
        new CoffeeProduct
        {
            Id = 13,
            Name = "Ube Cake",
            Description = "Delicious purple yam (ube) cake slice with sweet buttercream filling and purple yam aromatics.",
            Price = 20.00m,
            Category = "Burgers & Snacks",
            RoastLevel = "N/A",
            ImageUrl = "",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 35,
            StockQuantity = 30,
            Origin = "Filipino Sweet Classic",
            TastingNotes = new() { "Rich Ube", "Sweet Frosting", "Moist Sponge" },
            AvailableSizes = new() { "1 Slice" },
            AvailableTemperatures = new(),
            Ingredients = "Ube halaya, sponge cake flour, eggs, sugar, purple yam extract, buttercream.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Affordable sweet treat for ₱20 only!"
        },

        // === MOJITO DRINKS (SIGNATURE JMT MOJITOS) ===
        new CoffeeProduct
        {
            Id = 14,
            Name = "Blueberry Mojito",
            Description = "Chilled sparkling cocktail-style cooler with wild blueberries, fresh crushed mint leaves, fresh lime slices, and sparkling fizz over ice.",
            Price = 35.00m,
            Category = "Mojito Drinks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_blueberry_mojito_balloon.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 89,
            StockQuantity = 60,
            Origin = "JMT Signature Mojito Series",
            TastingNotes = new() { "Wild Blueberry", "Crushed Mint", "Zesty Lime", "Sparkling Fizz" },
            AvailableSizes = new() { "Stemmed Glass (16 oz)" },
            AvailableTemperatures = new() { "Iced" },
            Ingredients = "Blueberry syrup, fresh blueberries, crushed mint leaves, lime wedge, sparkling soda, ice.",
            Allergens = "None",
            BrewGuide = "Served chilled with fresh mint and blueberries. Super refreshing after a mountain road trip."
        },
        new CoffeeProduct
        {
            Id = 15,
            Name = "Red Berry Mojito",
            Description = "Vibrant red berry infusion with muddled mint leaves, sliced citrus, and sparkling cooler soda over crystal ice.",
            Price = 35.00m,
            Category = "Mojito Drinks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_berry_mojitos_four_glasses.png",
            IsFeatured = true,
            Rating = 4.9,
            ReviewCount = 71,
            StockQuantity = 55,
            Origin = "JMT Signature Mojito Series",
            TastingNotes = new() { "Ruby Red Berries", "Fresh Garden Mint", "Sparkling Citrus" },
            AvailableSizes = new() { "Stemmed Glass (16 oz)" },
            AvailableTemperatures = new() { "Iced" },
            Ingredients = "Red berry blend, mint leaves, fresh lemon slice, sparkling soda, ice.",
            Allergens = "None",
            BrewGuide = "Bright, bubbly, and thirst-quenching."
        },
        new CoffeeProduct
        {
            Id = 16,
            Name = "Strawberry Mojito",
            Description = "Sweet strawberry puree shaken with aromatic garden mint, lime juice, and fizzy soda in a tall chilled glass.",
            Price = 39.00m,
            Category = "Mojito Drinks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_strawberry_mojito_balloon.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 104,
            StockQuantity = 65,
            Origin = "JMT Top-Rated Mojito",
            TastingNotes = new() { "Sweet Strawberry", "Cool Mint", "Zesty Citrus" },
            AvailableSizes = new() { "Tall Goblet (16 oz)" },
            AvailableTemperatures = new() { "Iced" },
            Ingredients = "Ripe strawberry syrup, fresh lime, crushed mint, sparkling soda, ice.",
            Allergens = "None",
            BrewGuide = "Customer favorite mojito on the menu."
        },
        new CoffeeProduct
        {
            Id = 17,
            Name = "Lemon Mint Mojito",
            Description = "Classic zesty lemon and invigorating crushed fresh mint sparkling cooler over ice. Crisp, clean, and rejuvenating.",
            Price = 35.00m,
            Category = "Mojito Drinks",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_lemon_mint_mojito_stemmed_real.png",
            IsFeatured = true,
            Rating = 4.85,
            ReviewCount = 63,
            StockQuantity = 50,
            Origin = "JMT Signature Mojito Series",
            TastingNotes = new() { "Tart Lemon", "Cool Peppermint", "Effervescent Fizz" },
            AvailableSizes = new() { "Stemmed Glass (16 oz)" },
            AvailableTemperatures = new() { "Iced" },
            Ingredients = "Fresh lemon juice, mint leaves, simple syrup, sparkling soda water, ice.",
            Allergens = "None",
            BrewGuide = "Ultimate afternoon cooler in Tabon."
        },

        // === COFFEE DRINKS ===
        new CoffeeProduct
        {
            Id = 18,
            Name = "Spanish Latte",
            Description = "Smooth double espresso blended with velvety fresh milk and sweet condensed milk for an indulgent Spanish-style coffee.",
            Price = 45.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Medium",
            ImageUrl = "/images/jmt/jmt_spanish_latte_pour.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 92,
            StockQuantity = 50,
            Origin = "Barista Handcrafted",
            TastingNotes = new() { "Rich Espresso", "Sweet Condensed Milk", "Creamy Finish" },
            AvailableSizes = new() { "Hot Cup (8 oz)", "Iced Tall (16 oz)" },
            AvailableTemperatures = new() { "Hot", "Iced" },
            Ingredients = "Espresso, fresh full cream milk, sweetened condensed milk.",
            Allergens = "Dairy",
            BrewGuide = "Served hot with latte art or over ice."
        },
        new CoffeeProduct
        {
            Id = 19,
            Name = "Caramel Latte",
            Description = "Espresso and steamed milk swirled with golden sweet caramel syrup, finished with a smooth micro-foam layer.",
            Price = 45.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Medium",
            ImageUrl = "/images/jmt/jmt_caramel_latte_mug.png",
            IsFeatured = false,
            Rating = 4.9,
            ReviewCount = 57,
            StockQuantity = 45,
            Origin = "Barista Handcrafted",
            TastingNotes = new() { "Buttery Caramel", "Espresso Roast", "Velvet Milk" },
            AvailableSizes = new() { "Hot Cup (8 oz)", "Iced (16 oz)" },
            AvailableTemperatures = new() { "Hot", "Iced" },
            Ingredients = "Espresso, milk, caramel syrup.",
            Allergens = "Dairy",
            BrewGuide = "Delicious pairing with local bread or pancake."
        },
        new CoffeeProduct
        {
            Id = 20,
            Name = "Cappuccino",
            Description = "Traditional Italian-style cappuccino with equal parts robust espresso, rich steamed milk, and a thick airy crown of foam dusted with cocoa.",
            Price = 40.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Dark",
            ImageUrl = "/images/jmt/jmt_cocoa_latte_art_green_mug.png",
            IsFeatured = false,
            Rating = 4.85,
            ReviewCount = 49,
            StockQuantity = 40,
            Origin = "Classic Espresso Bar",
            TastingNotes = new() { "Dense Foam", "Bold Roast", "Cocoa Dust" },
            AvailableSizes = new() { "Standard Cup (8 oz)" },
            AvailableTemperatures = new() { "Hot" },
            Ingredients = "Double espresso shot, aerated steamed milk foam.",
            Allergens = "Dairy",
            BrewGuide = "Traditional thick foam texture."
        },
        new CoffeeProduct
        {
            Id = 21,
            Name = "Cafe Latte",
            Description = "Pure espresso with textured silky micro-foam milk poured with delicate tulip latte art in a ceramic cup.",
            Price = 39.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Medium",
            ImageUrl = "/images/jmt/jmt_artisan_tulip_latte_art.png",
            IsFeatured = true,
            Rating = 4.9,
            ReviewCount = 81,
            StockQuantity = 50,
            Origin = "Espresso Bar Standard",
            TastingNotes = new() { "Silky Micro-Foam", "Smooth Roast", "Latte Art" },
            AvailableSizes = new() { "Hot Cup (8 oz)" },
            AvailableTemperatures = new() { "Hot", "Iced" },
            Ingredients = "Fresh ground espresso, steamed milk.",
            Allergens = "Dairy",
            BrewGuide = "Classic comfort coffee for only ₱39!"
        },
        new CoffeeProduct
        {
            Id = 22,
            Name = "Americano",
            Description = "Double shot of fresh espresso diluted with hot filtered water, showcasing crisp chocolate and nutty aroma with a rich crema top.",
            Price = 39.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Dark",
            ImageUrl = "/images/jmt/jmt_takeaway_latte_trio_cups.png",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 53,
            StockQuantity = 60,
            Origin = "Pure Black Espresso",
            TastingNotes = new() { "Bold Black", "Clean Acidity", "Cocoa Finish" },
            AvailableSizes = new() { "Regular Cup (8 oz)" },
            AvailableTemperatures = new() { "Hot", "Iced" },
            Ingredients = "Espresso, hot purified water.",
            Allergens = "None",
            BrewGuide = "For coffee purists who like it straight and bold."
        },
        new CoffeeProduct
        {
            Id = 23,
            Name = "Macchiato",
            Description = "Bold espresso marked with a dollop of velvety steamed milk foam, delivering concentrated aroma and richness.",
            Price = 39.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Dark",
            ImageUrl = "/images/jmt/jmt_latte_tulip_mug.png",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 34,
            StockQuantity = 40,
            Origin = "Marked Espresso",
            TastingNotes = new() { "Intense Espresso", "Foam Mark", "Caramelized Crema" },
            AvailableSizes = new() { "Demitasse (4 oz)" },
            AvailableTemperatures = new() { "Hot" },
            Ingredients = "Espresso shot, small spoon of milk foam.",
            Allergens = "Dairy",
            BrewGuide = "Quick, intense caffeine boost."
        },

        // === SILOG & RICE MEALS ===
        new CoffeeProduct
        {
            Id = 24,
            Name = "CornSilog",
            Description = "Savory sauteed corned beef with onions, served with fragrant garlic fried rice and a sunny-side-up fried egg with crispy golden edges.",
            Price = 85.00m,
            Category = "Silog & Rice Meals",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_cornsilog_dish.png",
            IsFeatured = true,
            Rating = 4.9,
            ReviewCount = 64,
            StockQuantity = 40,
            Origin = "All-Day Filipino Breakfast",
            TastingNotes = new() { "Sauteed Corned Beef", "Garlic Sinangag", "Crispy Egg" },
            AvailableSizes = new() { "Full Silog Plate" },
            AvailableTemperatures = new(),
            Ingredients = "Corned beef, garlic, onion, steamed jasmine rice, fried egg.",
            Allergens = "Eggs",
            BrewGuide = "Hearty all-day meal cooked fresh."
        },
        new CoffeeProduct
        {
            Id = 25,
            Name = "Chicken Meal (Budget Friendly)",
            Description = "Crispy golden fried chicken served with 1 cup hot steamed rice and savory house chicken gravy.",
            Price = 79.00m,
            Category = "Silog & Rice Meals",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_chicken_meal.png",
            IsFeatured = true,
            Rating = 4.95,
            ReviewCount = 108,
            StockQuantity = 60,
            Origin = "JMT Daily Special",
            TastingNotes = new() { "Crispy Fried Chicken", "Steamed Rice", "Savory Gravy" },
            AvailableSizes = new() { "1pc Chicken + Rice" },
            AvailableTemperatures = new(),
            Ingredients = "Crispy chicken, jasmine rice, house gravy.",
            Allergens = "Wheat",
            BrewGuide = "Huge portion at an affordable ₱79 price point."
        },
        new CoffeeProduct
        {
            Id = 26,
            Name = "1-Pc Crispy Chicken Meal w/ Drink (Deluxe)",
            Description = "Signature 1pc crispy fried chicken meal with 1 cup hot rice, savory dipping sauce, and a colorful chilled drink (Butterfly Pea Lemon Refresher) in a tall glass.",
            Price = 135.00m,
            Category = "Silog & Rice Meals",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_fried_chicken_drumstick_meal.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 135,
            StockQuantity = 50,
            Origin = "JMT Deluxe Showcase Meal",
            TastingNotes = new() { "Crispy Chicken", "Steamed Rice", "Savory Gravy", "Refresher Drink" },
            AvailableSizes = new() { "1pc Chicken + Rice + Sauce + Drink" },
            AvailableTemperatures = new(),
            Ingredients = "Crispy chicken leg/thigh, rice, savory sauce, butterfly pea lemon drink, cucumber garnish.",
            Allergens = "Wheat",
            BrewGuide = "The complete JMT poster meal featured by Bisayang Explorer TV!"
        },
        new CoffeeProduct
        {
            Id = 27,
            Name = "LoafSilog",
            Description = "Pan-fried savory luncheon meat slices served with fragrant garlic rice, sunny egg, and fresh cucumber garnish.",
            Price = 65.00m,
            Category = "Silog & Rice Meals",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_loafsilog_dish.png",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 38,
            StockQuantity = 35,
            Origin = "Classic Silog Plate",
            TastingNotes = new() { "Crisp Meatloaf", "Garlic Rice", "Sunny Egg" },
            AvailableSizes = new() { "Full Silog Plate" },
            AvailableTemperatures = new(),
            Ingredients = "Meatloaf slices, garlic rice, egg, cucumber.",
            Allergens = "Eggs, Wheat",
            BrewGuide = "Comforting silog breakfast."
        },
        new CoffeeProduct
        {
            Id = 28,
            Name = "2pcs Egg Meal (Omelet Silog)",
            Description = "Fluffy pan-fried egg omelet served with hot steamed white rice, sliced cucumbers, and tomato catsup. (Scrambled egg meal also available at ₱45).",
            Price = 59.00m,
            Category = "Silog & Rice Meals",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_omelet_silog_plate_real.png",
            IsFeatured = false,
            Rating = 4.7,
            ReviewCount = 27,
            StockQuantity = 40,
            Origin = "Quick Light Meal",
            TastingNotes = new() { "Golden Egg Patty", "Steamed Rice", "Catsup Dip" },
            AvailableSizes = new() { "2pcs Egg Meal (₱59)", "Scrambled Egg Meal (₱45)" },
            AvailableTemperatures = new(),
            Ingredients = "Fresh eggs, butter, steamed rice, catsup, cucumber.",
            Allergens = "Eggs, Dairy",
            BrewGuide = "Light and wholesome breakfast."
        },

        // === JUICE & SHAKES ===
        new CoffeeProduct
        {
            Id = 29,
            Name = "Fresh Mango Shake (Large 16 oz)",
            Description = "Blended ripe Cebu carabao mango with sweet cream and crushed ice for a rich, sunny tropical treat. Served in a large 16 oz glass with fresh mango cubes on the rim.",
            Price = 65.00m,
            Category = "Juice & Shakes",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_mango_shake_16oz.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 88,
            StockQuantity = 50,
            Origin = "Cebu Mango Harvest",
            TastingNotes = new() { "Sweet Carabao Mango", "Velvet Cream", "Large 16 oz" },
            AvailableSizes = new() { "Large 16 oz (₱65)", "Regular Cup (₱55)" },
            AvailableTemperatures = new() { "Ice Blended" },
            Ingredients = "Fresh sweet mango, sweetened milk, crushed ice, fresh mango fruit garnish.",
            Allergens = "Dairy",
            BrewGuide = "Pure mango satisfaction on a warm sunny day in Dalaguete. Regular cup also available at ₱55."
        },
        new CoffeeProduct
        {
            Id = 30,
            Name = "Avocado Shake (Large 16 oz)",
            Description = "Creamy, nutrient-rich local avocado blended thick with condensed milk and crushed ice. Super rich and velvety in a large 16 oz serving.",
            Price = 75.00m,
            Category = "Juice & Shakes",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_avocado_shake_16oz.png",
            IsFeatured = true,
            Rating = 4.95,
            ReviewCount = 67,
            StockQuantity = 35,
            Origin = "Seasonal Fresh Avocado",
            TastingNotes = new() { "Creamy Butter Avocado", "Condensed Milk", "Large 16 oz" },
            AvailableSizes = new() { "Large 16 oz (₱75)", "Regular Cup (₱55)" },
            AvailableTemperatures = new() { "Ice Blended" },
            Ingredients = "Fresh ripe avocado, condensed milk, ice.",
            Allergens = "Dairy",
            BrewGuide = "Thick, rich, and naturally creamy. Regular cup also available at ₱55."
        },
        new CoffeeProduct
        {
            Id = 31,
            Name = "Banana Shake (Large 16 oz)",
            Description = "Naturally sweet Cavendish bananas blended with creamy milk and ice for a nourishing, smooth beverage in a large 16 oz glass.",
            Price = 55.00m,
            Category = "Juice & Shakes",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_banana_shake_16oz.png",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 44,
            StockQuantity = 40,
            Origin = "Local Banana Harvest",
            TastingNotes = new() { "Ripe Banana", "Creamy Milk", "Large 16 oz" },
            AvailableSizes = new() { "Large 16 oz (₱55)", "Regular Cup (₱50)" },
            AvailableTemperatures = new() { "Ice Blended" },
            Ingredients = "Fresh bananas, milk, syrup, ice.",
            Allergens = "Dairy",
            BrewGuide = "Great energizing refresher along the Tabon highway. Regular cup also available at ₱50."
        },
        new CoffeeProduct
        {
            Id = 32,
            Name = "Oreo Shake in Wine Goblet (w/ Choco Drizzle)",
            Description = "Our signature indulgence! Rich crushed Oreo cookies and vanilla cream blended thick, served in an elegant stemmed wine glass with dark chocolate syrup drizzled along the glass.",
            Price = 85.00m,
            Category = "Juice & Shakes",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_oreo_shake_wine_glass.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 142,
            StockQuantity = 50,
            Origin = "JMT Signature Glassware Presentation",
            TastingNotes = new() { "Crushed Oreo", "Chocolate Swirl", "Stemmed Wine Glass" },
            AvailableSizes = new() { "Stemmed Goblet Presentation" },
            AvailableTemperatures = new() { "Ice Blended" },
            Ingredients = "Oreo cookies, cream, chocolate fudge syrup, ice, whipped cream.",
            Allergens = "Dairy, Wheat",
            BrewGuide = "As seen in Bisayang Explorer TV's viral review of JMT Cafe!"
        },
        new CoffeeProduct
        {
            Id = 33,
            Name = "Oreo Shake (Crea Shake - Regular)",
            Description = "Delicious blended Oreo cookie frappe with sweet cream and crushed cookie crunch.",
            Price = 30.00m,
            Category = "Juice & Shakes",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_oreo_shake_duo_glasses_real.png",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 59,
            StockQuantity = 60,
            Origin = "Affordable Frappe Cup",
            TastingNotes = new() { "Cookies & Cream", "Affordable Price", "Ice Cold" },
            AvailableSizes = new() { "Regular Cup" },
            AvailableTemperatures = new() { "Ice Blended" },
            Ingredients = "Oreo cookies, milk, crushed ice.",
            Allergens = "Dairy, Wheat",
            BrewGuide = "Unbelievable value at only ₱30."
        },
        new CoffeeProduct
        {
            Id = 34,
            Name = "Ice Cold Beers (18+ Only)",
            Description = "Assorted chilled ice-cold beers, perfect for evening unwinding in our cool mountain terrace. Must be 18 years or older.",
            Price = 150.00m,
            Category = "Beer & Beverages (18+)",
            RoastLevel = "N/A",
            ImageUrl = "",
            IsFeatured = false,
            Rating = 4.8,
            ReviewCount = 31,
            StockQuantity = 40,
            Origin = "Chilled Beverages",
            TastingNotes = new() { "Crisp", "Chilled", "Malt Flavor", "18+ Only" },
            AvailableSizes = new() { "Bottle" },
            AvailableTemperatures = new() { "Chilled" },
            Ingredients = "Beer (Strictly 18+ only).",
            Allergens = "Barley/Gluten",
            BrewGuide = "Available for adult dine-in guests."
        },

        // === OFFICIAL JMT BURGER COMBOS (NEW POSTERS) ===
        new CoffeeProduct
        {
            Id = 35,
            Name = "Bacon Burger Combo w/ Fries & Drink",
            Description = "Flame-grilled bacon burger packed with pure beef, crispy smoked bacon, fresh tomato, and crisp lettuce, served with hot French fries and your choice of chilled beverage.",
            Price = 170.00m,
            Category = "Value Feast Combos",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_double_burger_fries_feast.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 118,
            StockQuantity = 50,
            Origin = "Official JMT Poster Combo",
            TastingNotes = new() { "Smoked Bacon Burger", "Crispy French Fries", "Beverage Included", "Flame-Grilled Beef" },
            AvailableSizes = new() { "w/ Blueberry Mojito (₱170)", "w/ Strawberry Mojito (₱175)", "w/ Mango Shake (₱185)" },
            AvailableTemperatures = new(),
            Ingredients = "Pure beef patty, crispy bacon, cheddar, tomato, lettuce, toasted bun, French fries, selected drink.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Best value complete combo in Dalaguete! Select your drink variation during inquiry."
        },
        new CoffeeProduct
        {
            Id = 36,
            Name = "American Burger Combo w/ Fries & Drink",
            Description = "Colossal American burger loaded with thick flame-grilled beef, melted cheddar cheese, lettuce, tomato, pickles, and steak sauce, served with French fries and a chilled drink.",
            Price = 215.00m,
            Category = "Value Feast Combos",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_american_burger_combo_set.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 94,
            StockQuantity = 40,
            Origin = "Official JMT American Feast",
            TastingNotes = new() { "Colossal Beef Burger", "Steak Sauce Glaze", "Seasoned Fries", "Blue Mojito or Shake" },
            AvailableSizes = new() { "w/ Blue Mojito Cooler (₱215)", "w/ Large Mango Shake (₱225)" },
            AvailableTemperatures = new(),
            Ingredients = "Thick beef patty, melted cheddar, lettuce, tomato, pickles, gourmet sauce, toasted bun, fries, beverage.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "A showstopping burger combo for big appetites visiting Dalaguete!"
        },

        // === VIETNAMESE EGG COFFEE & BARISTA ART ===
        new CoffeeProduct
        {
            Id = 37,
            Name = "Vietnamese Egg Coffee (Ca Phe Trung)",
            Description = "Traditional robust dark roast drip coffee crowned with a thick, velvety custard-like cloud of whipped egg foam and sweetened condensed milk.",
            Price = 65.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Dark",
            ImageUrl = "/images/jmt/jmt_latte_art_real_cup.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 76,
            StockQuantity = 45,
            Origin = "Vietnamese Artisanal Specialty",
            TastingNotes = new() { "Velvet Egg Custard", "Condensed Milk", "Dark Roasted Drip" },
            AvailableSizes = new() { "Standard Glass (8 oz)" },
            AvailableTemperatures = new() { "Hot", "Iced" },
            Ingredients = "Robusta dark roast drip coffee, fresh egg yolk, sweetened condensed milk.",
            Allergens = "Eggs, Dairy",
            BrewGuide = "Drink like a dessert! Sip the sweet golden cloud and dark coffee together."
        },
        new CoffeeProduct
        {
            Id = 38,
            Name = "Barista Signature Latte Art Special",
            Description = "Espresso poured with custom artisan latte art (Swan, Tulip, Rosetta, or Heart) handcrafted by our certified barista on the Dalaguete highway.",
            Price = 45.00m,
            Category = "Coffee Drinks",
            RoastLevel = "Medium-Dark",
            ImageUrl = "/images/jmt/jmt_twin_latte_art_mugs.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 160,
            StockQuantity = 50,
            Origin = "JMT Signature Barista Art",
            TastingNotes = new() { "Swan Latte Art", "Tulip Rosetta", "Crema Heart", "Barista Signature" },
            AvailableSizes = new() { "Hot Ceramic Mug (8 oz)", "Iced Tall (16 oz)" },
            AvailableTemperatures = new() { "Hot", "Iced" },
            Ingredients = "Espresso, freshly steamed textured milk micro-foam.",
            Allergens = "Dairy",
            BrewGuide = "Choose your favorite latte art when ordering at our counter in Purok 5 Tabon!"
        },

        // === NEW OFFICIALLY INTRODUCED MENU ITEMS FROM JMT CAFE FLYERS ===
        new CoffeeProduct
        {
            Id = 39,
            Name = "Cookies & Cream Frappe (JMT Signature Cup)",
            Description = "Creamy ice-blended vanilla frappe folded with crushed Oreo cookies, capped with whipped cream and cocoa biscuit crumb, served in the official JMT Cafe branded logo cup.",
            Price = 85.00m,
            Category = "Juice & Shakes",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_cookies_cream_frappe.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 112,
            StockQuantity = 50,
            Origin = "Official JMT Signature Cup",
            TastingNotes = new() { "Crushed Cookies", "Sweet Vanilla Cream", "JMT Branded Logo Cup" },
            AvailableSizes = new() { "Grande (16 oz)" },
            AvailableTemperatures = new() { "Ice Blended" },
            Ingredients = "Oreo cookies, cream, milk, vanilla base, ice, cocoa dusting.",
            Allergens = "Dairy, Wheat",
            BrewGuide = "Available now at JMT Cafe counter!"
        },
        new CoffeeProduct
        {
            Id = 40,
            Name = "Red Velvet Frappe (JMT Signature Cup)",
            Description = "Luxurious ruby-hued red velvet cake ice-blended frappe with subtle cocoa notes and sweet buttermilk cream, dusted with red velvet crumble in our official JMT Cafe logo cup.",
            Price = 85.00m,
            Category = "Juice & Shakes",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_red_velvet_frappe.png",
            IsFeatured = true,
            Rating = 5.0,
            ReviewCount = 98,
            StockQuantity = 50,
            Origin = "Official JMT Signature Cup",
            TastingNotes = new() { "Ruby Red Velvet", "Buttermilk Sweetness", "JMT Branded Logo Cup" },
            AvailableSizes = new() { "Grande (16 oz)" },
            AvailableTemperatures = new() { "Ice Blended" },
            Ingredients = "Red velvet cake infusion, cocoa, milk, ice, sweet cream, velvet dust.",
            Allergens = "Dairy, Wheat",
            BrewGuide = "Rich, colorful, and photo-ready!"
        },
        new CoffeeProduct
        {
            Id = 41,
            Name = "Classic Burger w/ Fries & Drinks Combo",
            Description = "Classic flame-grilled beef burger on a soft bun with cheddar cheese and fresh lettuce, served with hot golden French fries and 1 tall refreshing specialty drink.",
            Price = 150.00m,
            Category = "Value Feast Combos",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_classic_combo_meal_dish.png",
            IsFeatured = true,
            Rating = 4.95,
            ReviewCount = 87,
            StockQuantity = 45,
            Origin = "Official JMT Poster Combo",
            TastingNotes = new() { "Classic Beef Burger", "Shoestring Fries", "Choice of Drink" },
            AvailableSizes = new() { "w/ Blueberry Refresher (₱150)", "w/ Strawberry Refresher (₱155)", "w/ Mango Shake (₱165)" },
            AvailableTemperatures = new(),
            Ingredients = "Beef burger, cheddar, lettuce, fries, specialty cold beverage.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Great meal deal! Choose Blueberry (₱150), Strawberry (₱155), or Mango Shake (₱165)."
        },
        new CoffeeProduct
        {
            Id = 42,
            Name = "Buy 1 Take 1 Burger w/ 1 Drinks Combo",
            Description = "Two freshly grilled beef burgers with sliced cheese on toasted buns, served with 1 refreshing specialty cooler drink.",
            Price = 110.00m,
            Category = "Value Feast Combos",
            RoastLevel = "N/A",
            ImageUrl = "/images/jmt/jmt_b1t1_combo_meal_dish.png",
            IsFeatured = true,
            Rating = 4.95,
            ReviewCount = 125,
            StockQuantity = 60,
            Origin = "Official JMT Best-Value Duo",
            TastingNotes = new() { "2 Beef Burgers", "Sliced Cheese", "Specialty Drink" },
            AvailableSizes = new() { "w/ Blueberry Refresher (₱110)", "w/ Strawberry Refresher (₱115)", "w/ Mango Shake (₱125)" },
            AvailableTemperatures = new(),
            Ingredients = "2 beef burgers, cheese, 2 buns, specialty cold drink.",
            Allergens = "Wheat, Dairy, Eggs",
            BrewGuide = "Best value pair meal: 2 burgers + 1 drink! Blueberry (₱110), Strawberry (₱115), or Mango Shake (₱125)."
        }
    };

    public IEnumerable<CoffeeProduct> GetBestSellers(int count = 8)
    {
        lock (_lock)
        {
            var preferredIds = new[] { 1, 35, 26, 7, 29, 36, 14, 38 };
            var bestSellers = _products.Where(p => p.IsAvailable && preferredIds.Contains(p.Id))
                                      .OrderBy(p => Array.IndexOf(preferredIds, p.Id))
                                      .Take(count)
                                      .ToList();
            if (bestSellers.Count < count)
            {
                var others = _products.Where(p => p.IsAvailable && !preferredIds.Contains(p.Id) && p.IsFeatured)
                                      .Take(count - bestSellers.Count);
                bestSellers.AddRange(others);
            }
            return bestSellers;
        }
    }

    public IEnumerable<CoffeeProduct> GetAllProducts()
    {
        lock (_lock)
        {
            return _products.Where(p => p.IsAvailable).ToList();
        }
    }

    public IEnumerable<CoffeeProduct> GetFeaturedProducts()
    {
        lock (_lock)
        {
            return _products.Where(p => p.IsAvailable && p.IsFeatured).ToList();
        }
    }

    public IEnumerable<CoffeeProduct> GetFeaturedProducts(int count)
    {
        lock (_lock)
        {
            return _products.Where(p => p.IsAvailable && p.IsFeatured).Take(count).ToList();
        }
    }

    public IEnumerable<CoffeeProduct> GetProductsByCategory(string category)
    {
        lock (_lock)
        {
            if (string.IsNullOrWhiteSpace(category) || category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return _products.Where(p => p.IsAvailable).ToList();
            }
            return _products.Where(p => p.IsAvailable && p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

    public CoffeeProduct? GetProductById(int id)
    {
        lock (_lock)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }

    public IEnumerable<string> GetCategories()
    {
        lock (_lock)
        {
            return _products.Where(p => p.IsAvailable).Select(p => p.Category).Distinct().ToList();
        }
    }

    public IEnumerable<string> GetRoastLevels()
    {
        lock (_lock)
        {
            return _products.Where(p => p.IsAvailable && p.HasRoastLevel).Select(p => p.RoastLevel).Distinct().ToList();
        }
    }

    public IEnumerable<CoffeeProduct> SearchAndFilter(string? query, string? category, string? roastLevel, decimal? minPrice, decimal? maxPrice, string? sortBy)
    {
        return FilterProducts(category, roastLevel, minPrice, maxPrice, query, sortBy);
    }

    public IEnumerable<CoffeeProduct> FilterProducts(string? category, string? roastLevel, decimal? minPrice, decimal? maxPrice, string? searchQuery, string? sortBy)
    {
        lock (_lock)
        {
            var results = _products.Where(p => p.IsAvailable);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var q = searchQuery.Trim().ToLowerInvariant();
                results = results.Where(p => 
                    p.Name.ToLowerInvariant().Contains(q) || 
                    p.Description.ToLowerInvariant().Contains(q) ||
                    p.Category.ToLowerInvariant().Contains(q) ||
                    p.TastingNotes.Any(t => t.ToLowerInvariant().Contains(q)));
            }

            if (!string.IsNullOrWhiteSpace(category) && !category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                results = results.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(roastLevel) && !roastLevel.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                results = results.Where(p => p.RoastLevel.Equals(roastLevel, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                results = results.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                results = results.Where(p => p.Price <= maxPrice.Value);
            }

            results = sortBy?.ToLowerInvariant() switch
            {
                "price-asc" => results.OrderBy(p => p.Price),
                "price-desc" => results.OrderByDescending(p => p.Price),
                "rating" => results.OrderByDescending(p => p.Rating),
                "newest" => results.OrderByDescending(p => p.Id),
                _ => results.OrderByDescending(p => p.IsFeatured).ThenBy(p => p.Id)
            };

            return results.ToList();
        }
    }

    public bool UpdateStock(int productId, int quantityChange)
    {
        lock (_lock)
        {
            var item = _products.FirstOrDefault(p => p.Id == productId);
            if (item == null) return false;
            item.StockQuantity = Math.Max(0, item.StockQuantity + quantityChange);
            return true;
        }
    }

    public bool ToggleAvailability(int productId)
    {
        lock (_lock)
        {
            var item = _products.FirstOrDefault(p => p.Id == productId);
            if (item == null) return false;
            item.IsAvailable = !item.IsAvailable;
            return true;
        }
    }

    public void SaveProduct(CoffeeProduct product)
    {
        lock (_lock)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Category = product.Category;
                existing.RoastLevel = product.RoastLevel;
                existing.Description = product.Description;
                existing.StockQuantity = product.StockQuantity;
                existing.IsAvailable = product.IsAvailable;
                existing.IsFeatured = product.IsFeatured;
            }
            else
            {
                product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
                _products.Add(product);
            }
        }
    }
}
