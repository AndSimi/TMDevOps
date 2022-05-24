pipeline{
    agent any
    triggers{
        pollSCM("*/5 * * * *")
    }

    stages{
        stage("Build API"){
            steps{
                sh "echo 'We are building the API'"
                dir("MovieDB/Server"){
                    sh "dotnet build MovieDB.Server.csproj"
                }
                
            }
        }
        stage("Build frontend"){
            steps{
                sh "echo 'We are building the frontend'"
            }
        }
    }

}