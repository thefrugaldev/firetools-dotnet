﻿using System;
using System.Linq;
using FireTools.Utilities;

namespace FireTools.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose one of the following calculations: ");
            var actionList = Enum.GetValues(typeof(Action));
            Console.WriteLine();

            foreach (var action in actionList)
            {
                Console.WriteLine($"{action}: {(int)action}");
            }

            Console.WriteLine();

            int selectedAction;
            var input = Console.ReadLine();

            while (!int.TryParse(input, out selectedAction) || 
                   !Enum.IsDefined(typeof(Action), selectedAction))
            {
                Console.WriteLine("Please only select a number within the given values above.");
                input = Console.ReadLine();
            }

            Console.WriteLine();

            var calculatorType = (Action)selectedAction;

            switch (calculatorType)
            {
                case Action.SavingsRate:
                    CalculateSavingsRate();
                    break;
                case Action.Expenses:
                    CalculateExpensesOver10Years();
                    break;
                case Action.Retirement:
                    break;
                case Action.CompoundInterest:
                    break;
                case Action.AmountNeededToRetire:
                    break;
                case Action.MortgagePayoff:
                    break;
                case Action.PercentageToFinancialIndependence:
                    break;
                case Action.PerentageToRetireEarly:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //TODO: Calculate the following:
            //-> Retirement Calculator

            //-> Compound Interest Calculator
            //This could be used to see how much you could save for big items like college
            //by not spending on daily smaller items like a cup of coffee ($3.50 per day)

            //-> Calculate Yearly Spending and amount of money needed to retire
            //This assumes 4% withdraws (25x yearly spending)

            //-> Mortgage Amortization Calculator
            //Calculate how early you can pay off a mortgage with extra payments
            //Utilize and API?

            //Calculate % to Financial Independence
            //Amount saved (dedicated to retirement) / Target Amount

            //Calculate % to Retire Early
            //Could be the same amount as FI above, but some may want to work a few more years
            //for a better margin of safety

            Console.ReadLine();
        }

        private static void CalculateSavingsRate()
        {
            //-> Savings Rate
            //Total Savings (pre & post tax) / (After tax takehome pay + pre-tax contributions)
            Console.WriteLine("*** Calculating Savings Rate ***");
            Console.WriteLine("How much is your total take home pay, per paycheck?");
            var takeHomePay = decimal.Parse(Console.ReadLine());

            Console.WriteLine("How much do you contribute to retirement (pre-tax, including employer match) per paycheck?");
            var preTaxContributions = decimal.Parse(Console.ReadLine());

            Console.WriteLine("How much do you contribute to retirement (after tax) per paycheck?");
            var postTaxContributions = decimal.Parse(Console.ReadLine());

            var savingsRateService = new SavingsRateService(preTaxContributions, takeHomePay, postTaxContributions);
            var savingsRate = savingsRateService.Calculate() * 100;

            Console.WriteLine($"Your current savings rate is {decimal.Round(savingsRate, 2)}%");
        }

        private static void CalculateExpensesOver10Years()
        {
            Console.WriteLine("*** Calculating Expesnses ***");
            Console.WriteLine("*** The following calculation assumes a 7% growth over 10 years. ***");

            Console.WriteLine("Is the following expense a monthly (M), or weekly (W) expense?");
            var expenseType = Console.ReadLine().ToUpper();

            if (expenseType != "W" && expenseType != "M") return;

            Console.WriteLine("Please enter the recurring expense amount");
            var expenseAmount = decimal.Parse(Console.ReadLine());

            var expensesService = new ExpensesService(expenseAmount);
            var amountSaved = expenseType == "M" ? 
                expensesService.CalculateMonthlyExpenses() : 
                expensesService.CalculateWeeklyExpenses();

            Console.WriteLine($"If you invested your ${expenseAmount} instead, you would save ${amountSaved} after 10 years!");
        }
    }

    internal enum Action
    {
        SavingsRate = 1,
        Expenses,
        Retirement,
        CompoundInterest,
        AmountNeededToRetire,
        MortgagePayoff,
        PercentageToFinancialIndependence,
        PerentageToRetireEarly
    }
}
