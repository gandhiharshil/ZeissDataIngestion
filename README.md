# ZeissDataIngestion

This repository contains the source code for Zeiss data ingestion tool developed using ASP.NET. The tool allows users to upload and ingest data from various sources and stores it in a database for further processing.

##  Requirements

Visual Studio 2019 or later

.NET Framework 4.7.2 or later

SQL Server 2012 or later


## Getting Started
Clone the repository to your local machine.

git clone https://github.com/harshilg/ZeissDataIngestion.git

Open the solution file (ZeissDataIngestion.sln) in Visual Studio.

Build the solution to download the necessary dependencies and build the project.

Update the DocumentDB Connection string  in Startup and CosmosMachine Repository

Run the project in Visual Studio to start the application.

## Usage

Navigate to the homepage of the application.

Run the Application

Hit the Ingresion endpoint to start ingestion process

Stop to terminate

Hit data extractor endpoint to start fetchind data 

## Contributing
If you wish to contribute to this project, please fork the repository and submit a pull request. Before submitting a pull request, ensure that all tests pass and the code is properly formatted.

## License
This project is licensed under the MIT License. See the LICENSE file for more information.

## Contact
If you have any questions or feedback, please contact the project maintainer at harshil.gandhi@outlook.com

## Note
As an alternative way Data Ingestion is implemented as Middleware to run the process in background on startup.

To Use this approach ,Kindly uncomment the Backgroundservice middleware from startup

In this case Swagger will not be available therefore each endpoint will have to be queried through other interface.