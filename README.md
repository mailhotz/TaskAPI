# Boilerplate was setup using the following:
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio-code


# Description
As a Task manager, I need a system to maintain a list of tasks that I 
can assign to different people. This system needs to have several features. 
There should be an API that will allow me to manage tasks (creating, editing, 
and deleting). There should also be a microservice that will send an e-mail 
when an item has been created and/or if the task is overdue.

# Attributes
**Task**
* Id - int (required)
* Task - string (required)
* Details - string (optional)
* DueDate - DateTime (optional)
* CompletedOn - DateTime (optional)
* AssignedToName - string (optional)
* AssignedToEmail - string (required)

# Acceptance Criteria
* Uses Entity Framework (with migrations) for the ORM
* Has a REST API with an endpoints for managing tasks
    * Should use controllers (not minimal API)
    * No authentication or authorization necessary
* Has a microservice to send e-mails regarding the task
    * It doesn't actually have to send e-mails... just a placeholder for it
* Uses MassTansit to send/receive messages to/from microservice
    * Can use any message broker (e.g. RabbitMQ)