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
        'SharpPassBackendServices'
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
        'SharpPassBackendServices'
        '--template-body'
        "file://$PSScriptRoot/templates/BackendServices.yml"
        '--parameters'
        "file://$PSScriptRoot/../.local/deploy/BackendServicesParameters.json"
        '--capabilities'
        'CAPABILITY_NAMED_IAM'
    )
}

Deploy-Stack