pipeline {
    agent any

    environment {
        OCTOPUS_CLI = '/path/to/octo' // Update this to the path of the Octopus CLI on your Jenkins agent
        OCTOPUS_SERVER = 'https://your-octopus-server-url'
        OCTOPUS_API_KEY = credentials('OCTOPUS_API_KEY') // Store your API key in Jenkins credentials
        OCTOPUS_PROJECT = 'YourProjectName'
        OCTOPUS_ENVIRONMENT = 'YourEnvironmentName'
        OCTOPUS_RELEASE_VERSION = "1.0.${BUILD_NUMBER}"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    // Build the project using .NET CLI
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Package') {
            steps {
                script {
                    // Package the application for deployment
                    sh 'dotnet publish --configuration Release --output ./publish'
                    zip zipFile: 'artifact.zip', archive: true, dir: './publish'
                }
            }
        }

        stage('Push to Octopus') {
            steps {
                script {
                    // Push the package to Octopus Deploy
                    sh """
                    ${OCTOPUS_CLI} push \
                        --server=${OCTOPUS_SERVER} \
                        --apiKey=${OCTOPUS_API_KEY} \
                        --package=artifact.zip
                    """
                }
            }
        }

        stage('Create Release') {
            steps {
                script {
                    // Create a release in Octopus Deploy
                    sh """
                    ${OCTOPUS_CLI} create-release \
                        --server=${OCTOPUS_SERVER} \
                        --apiKey=${OCTOPUS_API_KEY} \
                        --project=${OCTOPUS_PROJECT} \
                        --version=${OCTOPUS_RELEASE_VERSION} \
                        --packageVersion=${OCTOPUS_RELEASE_VERSION}
                    """
                }
            }
        }

        stage('Deploy Release') {
            steps {
                script {
                    // Deploy the release to the specified environment
                    sh """
                    ${OCTOPUS_CLI} deploy-release \
                        --server=${OCTOPUS_SERVER} \
                        --apiKey=${OCTOPUS_API_KEY} \
                        --project=${OCTOPUS_PROJECT} \
                        --releaseNumber=${OCTOPUS_RELEASE_VERSION} \
                        --deployTo=${OCTOPUS_ENVIRONMENT}
                    """
                }
            }
        }
    }
}
