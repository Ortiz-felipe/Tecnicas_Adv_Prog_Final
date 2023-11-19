Feature: Province Management

  As a user of the Provinces API
  I want to be able to retrieve a list of provinces
  So that I can view information about different provinces

  Scenario: Retrieve all provinces successfully
    Given the Provinces API is available
    When I request a list of all provinces
    Then I should receive a list of provinces
    And each province should include details like name and other relevant information
