//start main//
int userInput = -1;

while(userInput != 3){//program keeps going until userInput is 3
    DisplayMenu();
    if(IsValidOption(ref userInput)){
        HotelFunctions(userInput);
    }
}
return;
//end main//


static void DisplayMenu(){//Displays the main menu
    System.Console.WriteLine("Enter 1 to Convert Currencies\nEnter 2 for Restaurant POS\nEnter 3 to Exit");
}

static bool IsValidOption(ref int userInput){//returns whether the user entered a valid menu option (1,2,3)
    string option = (Console.ReadLine());

    if (option == "1" || option == "2" || option == "3"){
        userInput = int.Parse(option);
        return true;
    }
    else {
        System.Console.WriteLine("Invalid Choice, please try again:");//reprompts the user if they entered an invalid menu option
        return false;
    }
}

static void HotelFunctions(int userInput){
    string currencyFrom = "";
    string currenyTo = "";
    double convertAmount = 0;

    if(userInput == 1){//Currency Conversion
        CurrencyExchangeMenu(ref currencyFrom, ref currenyTo, ref convertAmount);
        CurrencyExchange(currencyFrom, currenyTo, convertAmount);
    }
    else if(userInput == 2){//Restaurant POS
        RestaurantPOS();
    }
    else return;
}

static void CurrencyExchangeMenu(ref string currencyFrom, ref string currenyTo, ref double convertAmount){//Displays directions and reads in user input
    int flag = -1;
    System.Console.WriteLine("\nWhat currency would you like to convert from?\nPlease enter:");
    System.Console.WriteLine("U for US Dollar\nC for Canadian Dollar\nE for Euro\nI for Indian Rupee");
    System.Console.WriteLine("J for Japanese Yen\nM for Mexican Peso\nB for British Pound");
    currencyFrom = Console.ReadLine().ToUpper();
    System.Console.WriteLine("What amount would you like to convert?");

    while(flag != 1){ //error handling: reprompts user to enter a valid amount, stops after user enters a valid amount
        if(double.TryParse(Console.ReadLine(), out convertAmount)){
            flag = 1;
        }
        else{
            System.Console.WriteLine("Invalid input, please enter a valid amount:");
        }
    }

    System.Console.WriteLine("What currency would you like to convert to?\nPlease enter:");
    System.Console.WriteLine("U for US Dollar\nC for Canadian Dollar\nE for Euro\nI for Indian Rupee");
    System.Console.WriteLine("J for Japanese Yen\nM for Mexican Peso\nB for British Pound");
    currenyTo = Console.ReadLine().ToUpper();
}

static void CurrencyExchange(string currencyFrom, string currencyTo, double convertAmount){//Does the math for converting from and to any currency on the list
    double convertedAmount = 0;

    //checks validity of currencyFrom and currencyTo
    if(currencyFrom == "U" || currencyFrom == "C" || currencyFrom == "E" || currencyFrom == "I" || currencyFrom == "J" || currencyFrom == "M" || currencyFrom == "B"){
        if(currencyTo == "U" || currencyTo == "C" || currencyTo == "E" || currencyTo == "I" || currencyTo == "J" || currencyTo == "M" || currencyTo == "B"){
            convertedAmount = convertAmount/ExchangeRate(currencyFrom);//converts currencies to USD
            convertedAmount = convertedAmount * ExchangeRate(currencyTo);//converts from USD to target currency
            System.Console.WriteLine("The exchange amount is " + Math.Round(convertedAmount,2) + "\n"); //displays the amount after conversion rounded to 2 decimal places
        }
        else{
            System.Console.WriteLine("Invalid input(s), please try again:");
            HotelFunctions(1); //reprompts the user to restart Currency Conversion since they entered invalid input(s)
        }
    }
    else {
        System.Console.WriteLine("Invalid input(s), please try again:");//displays error message
        HotelFunctions(1); //reprompts the user to restart Currency Conversion since they entered invalid input
    }
}

static double ExchangeRate(string currency){//list of exchange rates that are used in CurrencyExchange()
    double exchangeRate = 0;

    if(currency == "U"){
        exchangeRate = 1;
    }
    else if(currency == "C"){
        exchangeRate = 1.34;
    }
    else if(currency == "E"){
        exchangeRate = 0.93;
    }
    else if(currency == "I"){
        exchangeRate = 82.24;
    }
    else if(currency == "J"){
        exchangeRate = 131.20;
    }
    else if(currency == "M"){
        exchangeRate = 18.98;
    }
    else if(currency == "B"){
        exchangeRate = 0.83;
    }
    return exchangeRate;
}

static void RestaurantPOS(){
    double foodTotal;
    double alcoholTotal;
    double totalBill;
    double tipAmount;
    double totalAmountOwed;
    double amountPaid;
    double changeDue;

    System.Console.WriteLine("Please enter the food total:");
    foodTotal = double.Parse(Console.ReadLine());
    System.Console.WriteLine("Please enter the alcohol total:");
    alcoholTotal = double.Parse(Console.ReadLine());

    totalBill = (foodTotal + alcoholTotal) * 1.09;// total bill is taxed at 9%
    tipAmount = foodTotal * 0.18; // calculates 18% tip on non-alcohol products
    totalAmountOwed = totalBill + tipAmount; //total amount due is the sum of the total bill and the 18% tip on non-alcohol products

    System.Console.WriteLine("The total amount due is " +totalAmountOwed);
    System.Console.WriteLine("Please enter the amount paid");
    amountPaid = double.Parse(Console.ReadLine());

    if(amountPaid >= totalAmountOwed){//checks if amountPaid is greater than totalAmountOwed
        changeDue = amountPaid - totalAmountOwed;
        System.Console.WriteLine("The amount of change due is "+ Math.Round(changeDue,2) + "\n");//displays change due
    }
    else if(amountPaid < totalAmountOwed){//user entered an amount less than totalAmountOwed
        System.Console.WriteLine("Error, amount paid is less than total amount owed\nPlease try again:");//displays error message
        HotelFunctions(2); //reprompts the user to RestaurantPOS() since they entered invalid input
    }
}
