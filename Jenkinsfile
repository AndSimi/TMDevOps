pipeline{
    agent any
    triggers{
        pollSCM("*/5 * * * *")
    }

    stages{
        stage("Build API"){
            steps{
                sh "echo 'We arer building the API'"
                sh "dotnet build MovieDB.sln"
            }
        }
        stage("Build frontend"){
            steps{
                sh "echo 'We are building the frontend'"
            }
        }
    }

}