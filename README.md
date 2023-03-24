# ServerlessPass

Welcome to ServerlessPass, an API that allows you to synchronize your LessPass password profiles with ease and zero hassle.
This documentation will guide you through the installation process of ServerlessPass.

### Requirements

Before installing ServerlessPass, please make sure you have the following requirements:

* An AWS account
* A domain name managed by Route 53 hosted zone

### Installation

Follow these steps to install ServerlessPass:

#### Step 1: Provision AWS resources

To create a stack, use the CloudFormation template provided in the [BackendServices.yml](deploy/templates/BackendServices.yml) file.
The stack will automatically create all the necessary resources in your AWS account.

Please make sure to provide the following parameters:
* ArtifactsBucketName: Provide a name for a new S3 bucket to store the binaries of your built application
* DomainName: Provide a domain name for your API, such as "api.lesspass.mydomain.com" or "lp.mydomain.com"
* DomainHostedZoneId: Provide a HostedZoneId of your domain, which can be found in Route53 -> Hosted Zones
* Stage: Use this parameter to deploy multiple stages of ServerlessPass.
Leave it at the default value if this is your first ServerlessPass stack
* CrossOriginDomains: Use this parameter to configure the Access-Control-Allow-Origin header.
Leave it at the default value unless you want to access your API from an additional domain

Wait for the stack to reach the "CREATE_COMPLETE" status

#### Step 2: Deploy the latest version of the code
To deploy the latest version of the code, start the build of the `ServerlessPass${Stage}BackendDeploy` CodeBuild project.
Wait for the build to finish and ensure that the status is "Succeeded"

#### Step 3: Configure username and password
Open the Cognito user pool `ServerlessPass${Stage}Users` and find the "User pool ID".
Then, open CloudShell and run the following two commands to create a user:

`aws cognito-idp admin-create-user --user-pool-id ${UserPoolId} --username ${YourEmailAddress} --message-action SUPPRESS`

`aws cognito-idp admin-set-user-password --user-pool-id ${UserPoolId} --username ${YourEmailAddress} --password ${YourPreferredPassword}`

Please ensure that you replace `${UserPoolId}`, `${YourEmailAddress}`, and `${YourPreferredPassword}` with appropriate values.

#### Step 4: Configure LessPass
Open the LessPass extension or mobile app and configure it to use your `${DomainName}`, then sign in using `${YourEmailAddress}` and `${YourPreferredPassword}`.
You're all set!

Thank you for choosing ServerlessPass.
If you have any questions or issues, please do not hesitate to file an issue