$targetFolder = "CS6334axm230407"

# Get all unique file extensions
$extensions = Get-ChildItem -Recurse -File $targetFolder | 
              Where-Object { $_.Extension -ne "" } |
              Select-Object -ExpandProperty Extension |
              ForEach-Object { $_.TrimStart('.') } |
              Sort-Object -Unique

foreach ($ext in $extensions) {
    $pattern = "$targetFolder/*.$ext"
    Write-Output "git lfs track `"$pattern`""
    git lfs track "$pattern"
}

git add .gitattributes
