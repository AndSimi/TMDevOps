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
                    when{
                        anyOf {
                            changeset "MovieDB/Server/**"
                            changeset "MovieDB/Shared/**"
                            changeset "MovieDB/Client/**"
                }
                    }
                    steps{
                        sh "echo 'We are building the API'"
                        dir("MovieDB/Server"){
                            sh "dotnet build --configuration Release"
                        }
                        dir("MovieDB/Shared"){
                            sh "dotnet build --configuration Release"
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
                            sh "dotnet build --configuration Release"
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

        stage("Clean"){
            steps{
                script{
                    try{
                        sh "docker-compose down"
                    }
                    finally{}
                }
            }
        }



        stage("Deploy"){
            steps{
                sh "docker-compose up -d --build"
            }
            
        }
    }

    post{
        changed{
            sh "echo 'Pipeline finished'"
        }
    }





}