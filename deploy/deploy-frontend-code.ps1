param(
    [string] $Stage = 'Dev'
)

$Local = "$PSScriptRoot/../.local"

function Publish-Code {
    & npm @(
        'run'
        '--prefix'
        "${PSScriptRoot}/../code/frontend"
        'build'
    )
}

function Deploy-Code {
    $bucketName = Get-ExportValue -Name "ServerlessPass${Stage}FrontendArtifactsBucketName"

    & aws @(
        's3'
        'sync'
        "$Local/Frontend"
        "s3://$bucketName"
    )
}

function Update-Cache {
    $distributionId = Get-ExportValue -Name "ServerlessPass${Stage}CloudFrontDistributionId"

    $invalidationId = & aws @(
        'cloudfront'
        'create-invalidation'
        '--distribution-id'
        $distributionId
        '--paths'
        '/*'
        '--query'
        'Invalidation.Id'
        '--output'
        'text'
    )

    & aws @(
        'cloudfront'
        'wait'
        'invalidation-completed'
        '--distribution-id'
        $distributionId
        '--id'
        $invalidationId
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

Publish-Code
Deploy-Code
Update-Cache