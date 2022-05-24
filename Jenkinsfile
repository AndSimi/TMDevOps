pipeline{
    agent any
    triggers{
        pollSCM("* * * * *")
    }

    stages{
        stage("Build"){
            steps{
                echo "Yay we are running :D"
            }
        }
    }

}