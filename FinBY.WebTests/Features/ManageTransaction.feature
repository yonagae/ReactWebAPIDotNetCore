Feature: Manage Tansaction
As a developer
I want to ensure functionality is working end to end

@mytag
Scenario:view list of transactions
Given I have navigated to FinBy website
When I press the transactions menu option
Then I should view the list of transactions


Scenario:create new transactions
Given I have navigated to FinBy website
And I press the transactions menu option
And I press the new transaction button
And fill the transaction info
When I click on the new transaction button
Then a new transaction with the inputed data should be created
