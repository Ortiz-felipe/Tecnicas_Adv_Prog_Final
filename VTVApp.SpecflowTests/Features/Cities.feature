Feature: City Management

  As a user of the Cities API
  I want to be able to retrieve a list of cities
  So that I can view information about different cities

  Scenario: Retrieve all cities successfully
    Given the Cities API is available
    When I request a list of all cities
    Then I should receive a list of cities
    And each city should include details like name and province