# database_configuration

## Motivation

I wanted to use this project as an opportunity to try out new (to me) methods of processing and managing data. 

## Description

A CSV file was uploaded into Azure Databricks. PySpark was then used for all of the data processing to create the key metrics. The finished data was then exported into an Azure SQL database. An Azure Function App was then created to return values based on a key value pair entered by the user. This was an attempt to mimic a front end application requesting specific information.

## Visuals

![Databricks Screenshot](https://user-images.githubusercontent.com/65408615/100010431-432c6e00-2d9e-11eb-85f6-dd0011f2b707.png)
Processed dataframe from Databricks

![Postman Screenshot](https://user-images.githubusercontent.com/65408615/100010546-69eaa480-2d9e-11eb-9503-5a4c76862d86.png)
Postman hitting the URL to return Kyrie Irving's player_id
