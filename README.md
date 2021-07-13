# DurableFunctionsApp

### This is a durable function app that finds the ***grade*** of students based on their percentages and also show the ***toppers*** from each division.
Check the function here - [https://dfamandeep.azurewebsites.net/api/Function1_HttpStart?](https://dfamandeep.azurewebsites.net/api/Function1_HttpStart?)

## How to use the app.

1. Go to this [link](https://dfamandeep.azurewebsites.net/api/Function1_HttpStart?), where the Http Trigger for the Durable function gets initiated. 
2. You will see a JSON object that show the response of the GET request. This contains some useful URLs.
3. To see the status of the function, copy the value of ***statusQueryGetUri*** and paste it new tab.
4. You will see the **grades** of student and the **toppers** in the output key of the response. *(If you do not see the output of the function wait and reload the page after 2 or 3 seconds.)*


### Example output - 
![image](https://user-images.githubusercontent.com/83014804/125511356-19138bf0-e14d-4f7b-9eca-7f85e9303d3b.png)

### Student List Used
```
  public List<Student> students = new List<Student>()
        {
            new Student(){ id=1, name="Mandeep Baghel", percentage= 78 },
            new Student(){ id=2, name="Mohit Patel", percentage= 76.12 },
            new Student(){ id=3, name="Mahim Panchal", percentage= 84 },
            new Student(){ id=4, name="Ruchir Gavshinde", percentage= 96 },
            new Student(){ id=5, name="Ayushi Kumar", percentage= 58.65 },
            new Student(){ id=6, name="Mayur Rathore", percentage= 68.12 },
            new Student(){ id=7, name="Mayuri Birle", percentage= 49 },
            new Student(){ id=8, name="Bhavesh Bhagwat", percentage= 54.2 },
            new Student(){ id=9, name="Divyanshu Chaturvedi", percentage= 87.45 },
            new Student(){ id=10, name="Nikhil Kumar", percentage= 74.12 },
            new Student(){ id=11, name="Isha Joshi", percentage= 27},
            new Student(){ id=12, name="Damodar Punarswami", percentage = 74.20 },
            new Student(){ id=13, name="Mukul Borkar", percentage= 89.12 },
            new Student(){ id=14, name="Mohit Ratnawat", percentage= 78.21 },
            new Student(){ id=15, name="Ankit Rathod", percentage= 58.41 },
            new Student(){ id=16, name="Sandeep Sharma", percentage= 68.12 },
            new Student(){ id=17, name="Mansha Joshi", percentage= 30},
            new Student(){ id=18, name="Yuvraj Yadav", percentage= 85.2 },
            new Student(){ id=19, name="Harshal Ogale", percentage= 74.45 },
            new Student(){ id=20, name="Pushpak Patidar", percentage= 74 },
        }; 
```

