$Local = "$PSScriptRoot/../.local"

function Publish-Code {
    & dotnet @(
        'publish'
        "$PSScriptRoot/../code/backend/Server.AWS/Server.AWS.csproj"
        '-c'
        'Release'
        '-o'
        "$Local/Publish"
    )
}

function Publish-Template {
    $bucketName = Get-ExportValue -Name 'SharpPassArtifactsBucketName'

    & aws @(
        'cloudformation'
        'package'
        '--template-file'
        "$PSScriptRoot/templates/BackendLambda.yml"
        '--s3-bucket'
        $bucketName
        '--output-template-file'
        "$Local/BackendLambda.yml"
    )
}

function Get-ExportValue {
    param(
        [string] $name
    )

    & aws @(
        'cloudformation'
        'list-exports'
        '--query'
        "(Exports[?Name=='$name'].Value)[0]"
        '--output'
        'text'
    )
}

function Deploy-Stack {
    $stack = Find-Stack
    if ($null -eq $stack) {
        New-Stack
    }
    else {
        Update-Stack
    }
}

function Find-Stack {
    & aws @(
        'cloudformation'
        'describe-stacks'
        '--stack-name'
        'SharpPassLambda'
    ) 2>$null
}

function New-Stack {
    Write-Stack -Command 'create-stack'
    Wait-Stack -Condition 'stack-create-complete'
}

function Update-Stack {
    Write-Stack -Command 'update-stack'
    Wait-Stack -Condition 'stack-update-complete'
}

function Write-Stack {
    param(
        [string] $command
    )

    & aws @(
        'cloudformation'
        $command
        '--stack-name'
        'SharpPassLambda'
        '--template-body'
        "file://$Local/BackendLambda.yml"
    )
}

function Wait-Stack {
    param(
        [string] $condition,
        [string] $stackName
    )

    & aws @(
        'cloudformation'
        'wait'
        $condition
        '--stack-name'
        'SharpPassLambda'
    )
}

function New-Deployment {
    $apiId = Get-ExportValue -Name 'SharpPassApiGatewayId'

    & aws @(
        'apigateway'
        'create-deployment'
        '--rest-api-id'
        $apiId
        '--stage-name'
        'prod'
    )
}

Publish-Code
Publish-Template
Deploy-Stack
New-Deployment