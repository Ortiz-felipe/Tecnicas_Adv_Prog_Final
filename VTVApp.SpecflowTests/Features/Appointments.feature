Feature: Appointment Management
    In order to manage appointments effectively
    As a user of the API
    I want to be able to create, retrieve, update, and delete appointments

Scenario: Retrieve all appointments
	When I request a list of all appointments
	Then I should receive a list of appointments
	And each appointment should include details like date, time, and status

Scenario: Retrieve available slots for the current day
	Given I have the current date in YYYY-MM-DD format
	When I request available appointment slots for this date
	Then I should receive a list of available slots for that day

Scenario: Create a new appointment
	When I create a new appointment with the necessary details
	Then the appointment should be successfully created
	And I should receive the details of the new appointment

Scenario: Retrieve a specific appointment by ID
	Given an appointment with ID "appointmentId" exists
	When I request the details of the appointment with ID "appointmentId"
	Then I should receive the details of the specified appointment

Scenario: Retrieve the latest appointment for a user
	Given a user with ID "034030b9-d2a3-4a4c-b70e-852b6ae9dd37" has recent appointments
	When I request the latest appointment for user with ID "034030b9-d2a3-4a4c-b70e-852b6ae9dd37"
	Then I should receive the details of the most recent appointment for that user

Scenario: Retrieve all appointments for a user
	Given a user with ID "034030b9-d2a3-4a4c-b70e-852b6ae9dd37" exists
	When I request all appointments for user with ID "userId"
	Then I should receive a list of appointments for that user

#Scenario: Retrieve all appointments that need a recheck
#    When I request a list of appointments that require a recheck
#    Then I should receive a list of such appointments
#
#Scenario: Complete an appointment
#    Given an appointment with ID "appointmentId" exists
#    When I mark the appointment with ID "appointmentId" as completed
#    Then the status of the appointment should be updated to "Completed"
#
#Scenario: Cancel an appointment
#    Given an appointment with ID "appointmentId" exists
#    When I cancel the appointment with ID "appointmentId"
#    Then the appointment should be cancelled successfully
#
#Scenario: Reschedule an appointment
#    Given an appointment with ID "appointmentId" exists
#    When I reschedule the appointment with ID "appointmentId" to a new time and date
#    Then the appointment should be rescheduled successfully
