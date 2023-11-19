Feature: User Management

  Scenario: Register a new user
    Given I have user registration details
    When I register the user
    Then the user should be registered successfully
    And the user details should be returned

  Scenario: Get a user by their ID
    Given I have a valid user ID
    When I request details for the user by ID
    Then the user's details should be returned

  Scenario: Get a user with an invalid ID
    Given I have an invalid user ID
    When I request details for the invalid user by ID
    Then I should receive a not found response
#
#  Scenario: Update a user's details
#    Given I have a valid user ID and updated user details
#    When I update the user's details
#    Then the user's details should be updated successfully
#
#  Scenario: Delete a user
#    Given I have a valid user ID
#    When I delete the user
#    Then the user should be deleted successfully
#

 Scenario: List all users
    Given there are users in the system
    When I request a list of all users
    Then I should receive a list of users

  Scenario: Login user
    Given I have valid login credentials
    When I log in
    Then I should be logged in successfully
    And receive a user authentication token
#
#  Scenario: Login with invalid credentials
#    Given I have invalid login credentials
#    When I attempt to log in
#    Then the login should be unsuccessful
