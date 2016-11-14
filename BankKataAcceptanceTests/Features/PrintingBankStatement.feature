Feature: PrintingBankStatement
	In order to know the state of the my account
	As a client
	I want to print my statement

Scenario: Print statement lines
	Given A deposit of 10 on 10-01-2012
	And A deposit of 20 on 13-01-2012
	And A withdrawal of 5 on 14-01-2012
	When I print my bank statement
	Then My statement is
		"""
		date | credit | debit | balance
		14/01/2012 | | 5.00 | 25.00
		13/01/2012 | 20.00 | | 30.00
		10/01/2012 | 10.00 | | 10.00
		"""
