# VendingMachine

**Lexicon Assignment 4: Vending Machine**

Your assignment for this week is to make a vending machine. It should be able to take requests from the user as to what product it is supposed to return. It should also take payment, and send the product back to the user with an appropriate amount of change.

## Required Features:

* Money should be input in fixed denominations: 1kr, 5kr, 10kr, 20kr, 50kr, 100kr, 500kr and 1000kr.

* The user should be able to put any amount of money in, adding to the "money pool".

* The money inserted MUST be of a valid denomination.

* The user should be able to buy any number of products, as long as they have the money for it in the machine. 

* When the user decides to stop buying things, the remaining money should be returned as change.

* The vending machine should have at least three different types of product.

* All products must be stored in one collection only.

* Once a product has been purchased, the user should be able to use it, showing a message about how it is used (drink the drink, eat the snack, etc.)

## Code Requirements:

* Money denominations should be defined as an array of integers (readonly).

* The vending machine class shall implement the following interface IVending:
    - Purchase, to buy a product.
    - ShowAll, show all products.
    - InsertMoney, add money to the pool.
    - EndTransaction, return money left in appropriate amount of change (Dictionary).

* Each product type should be its own class.

* These classes should all inherit from the same abstract class, to get the methods they have in common. This base class is the type the vending machine itself should look for.

* The common methods should include at least: 
    - Examine, to show the product's price and info 
    - Use, to put the product to use once it has been purchased (output a string message how to use the product)

* Unit test your code.
