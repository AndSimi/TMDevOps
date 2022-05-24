pipeline{
    agent any
    triggers{
        pollSCM("*/5 * * * *")
    }

    stages{
        stage("Build"){
            parallel{      
                stage("Build API"){
                    steps{
                        sh "echo 'We are building the API'"
                        dir("MovieDB/Server"){
                            sh "dotnet build MovieDB.Server.csproj"
                        }
                    }
                    post{
                        always{
                            sh"echo 'Building API finished'"
                        }
                        success{
                            sh"echo 'Building API successful'"
                        }
                        failure{
                            sh"echo 'Building API failed'"
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
      
    }
}