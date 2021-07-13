# DurableFunctionsApp

### This is a durable function app that finds the ***grade*** of students based on their percentages and also show the ***toppers*** from each division.
Check the function here - [https://dfamandeep.azurewebsites.net/api/Function1_HttpStart?](https://dfamandeep.azurewebsites.net/api/Function1_HttpStart?)

## How to use the app.

1. Go to this [link](https://dfamandeep.azurewebsites.net/api/Function1_HttpStart?), where the Http Trigger for the Durable function gets initiated. 
2. You will see a JSON object that show the response of the GET request. This contains some useful URLs.
3. To see the status of the function, copy the value of ***statusQueryGetUri*** and paste it new tab.
4. You will see the **grades** of student and the **toppers** in the output key of the response. *(If you do not see the output of the function wait and reload the page after 2 or 3 seconds.)*
