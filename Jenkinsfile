pipeline{
    agent any
    triggers{
        pollSCM("*/5 * * * *")
    }
    parameters{
        string(name: "Person", defaultValue: "GitHub", description: "Who are you?")
    }

    stages{
        stage("Startup"){
            steps{
                sh "echo 'Build started by: ${params.Person}'"
                dir("MovieDB.Server.Test"){
                    sh "rm -rf TestResults"
                }
            }
            
        }
        stage("Build"){
            parallel{      
                stage("Build API"){
                    steps{
                        sh "echo 'We are building the API'"
                        dir("MovieDB/Server"){
                            sh "dotnet build MovieDB.Server.csproj"
                        }
                        dir("MovieDB/Shared"){
                            sh "dotnet build MovieDB.Shared.csproj"
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

        stage("Test"){
            steps(){
                sh "echo 'We are unit testing the API'"
                dir("MovieDB.Server.Test"){
                    sh "dotnet add package coverlet.collector"
                    sh "dotnet test --collect:'XPLat Code Coverage'"
                }
            }
            post{
                success{
                    publishCoverage adapters: [coberturaAdapter(path:"MovieDB.Server.Test/TestResults/*/coverage.cobertura.xml")]
                    archiveArtifacts "MovieDB.Server.Test/TestResults/*/coverage.cobertura.xml"
                }
            }
        }


    }
    post {
            changed {
                sh "echo 'Pipeline finished'"
            }
        }
}