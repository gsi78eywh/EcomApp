$text = Get-Content c:\EcomApp\LusiTrack\Services\CoffeeCatalogService.cs -Raw
$pattern = 'Id\s*=\s*(\d+),\s*Name\s*=\s*"([^"]+)",\s*Description\s*=\s*"([^"]+)",\s*Price\s*=\s*([0-9.]+)m,\s*Category\s*=\s*"([^"]+)",(?:.*?ImageUrl\s*=\s*"([^"]*)")?'
$matches = [regex]::Matches($text, $pattern, [System.Text.RegularExpressions.RegexOptions]::Singleline)
Write-Host "Found $($matches.Count) products"
foreach ($m in $matches) {
    Write-Host "$($m.Groups[1].Value) | $($m.Groups[2].Value) | ₱$($m.Groups[4].Value) | $($m.Groups[5].Value) | $($m.Groups[6].Value)"
}
