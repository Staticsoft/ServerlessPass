param(
    [string] $Stage = 'Dev'
)

$StackName = "ServerlessPassFrontendServices$Stage"
$ParametersFileName = "FrontendParameters$Stage.json"

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
        $StackName
    ) 2>$null
}

function New-Stack {
    Write-Stack -Command 'create-stack'
}

function Update-Stack {
    Write-Stack -Command 'update-stack'
}

function Write-Stack {
    param(
        [string] $command
    )

    & aws @(
        'cloudformation'
        $command
        '--stack-name'
        $StackName
        '--template-body'
        "file://$PSScriptRoot/templates/FrontendServices.yml"
        '--parameters'
        "file://$PSScriptRoot/../.local/deploy/$ParametersFileName"
        '--capabilities'
        'CAPABILITY_NAMED_IAM'
    )
}

Deploy-Stack