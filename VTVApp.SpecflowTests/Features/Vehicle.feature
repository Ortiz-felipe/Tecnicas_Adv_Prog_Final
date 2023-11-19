Feature: Vehicle Management

  As a user of the Vehicles API
  I want to be able to manage vehicle information
  So that I can maintain records of different vehicles

  Scenario: Register a new vehicle
    Given a user with a specific ID
    When I submit a request to create a vehicle with valid details for the user
    Then the vehicle should be created successfully
    And I should receive the details of the newly created vehicle

  Scenario: Retrieve a specific vehicle by ID
    Given a vehicle with a specific ID exists
    When I request the details of the vehicle with that ID
    Then I should receive the details of the specified vehicle

  Scenario: Retrieve all vehicles for a user
    Given a user with a specific ID has registered vehicles
    When I request a list of all vehicles for the user
    Then I should receive a list of vehicles associated with that user

  Scenario: Retrieve favorite vehicle for a user
    Given a user with a specific ID has marked a vehicle as favorite
    When I request the favorite vehicle for the user
    Then I should receive the details of the user's favorite vehicle
