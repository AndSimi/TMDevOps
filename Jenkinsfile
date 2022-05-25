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
                        dir("MovieDB/Client"){
                            sh "dotnet build MovieDB.Client.csproj"
                        }
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
                    publishCoverage adapters: [cobertura('MovieDB.Server.Test/TestResults/*/coverage.cobertura.xml')], sourceFileResolver: sourceFiles('NEVER_STORE')
                    archiveArtifacts "MovieDB.Server.Test/TestResults/*/coverage.cobertura.xml"
                }
            }
        }



        stage("Deploy"){
            parallel{
                stage("Frontend"){
                    steps{
                        dir("MovieDB/Client/wwwroot"){
                            sh "docker build -t movie-web ."
                            sh "docker run --name movie-web-container -d -p 8090:80 movie-web"
                        }
                    }
                }
                stage("API"){
                    steps{
                        dir("MovieDB/Server"){
                            sh "docker build -t movie-api ."
                            sh "docker run --name movie-api-container -d -p 8091:80 movie-api"
                        }
                    }
                    

                }
                stage("Database"){
                    steps{
                        sh "docker run --name movie-db-container -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrongP@ssword' -p 8092:1433 -d mcr.microsoft.com/mssql/server"

                    }
                }



            }
        }
    }

    post{
        changed{
            sh "echo 'Pipeline finished'"
        }
    }



}