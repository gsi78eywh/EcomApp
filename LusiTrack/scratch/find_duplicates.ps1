$text = Get-Content "c:\EcomApp\LusiTrack\Services\CoffeeCatalogService.cs" -Raw
$pattern = 'Id\s*=\s*(\d+),\s*Name\s*=\s*"([^"]+)",(?:(?!new CoffeeProduct).)*?ImageUrl\s*=\s*"([^"]*)"'
$matches = [regex]::Matches($text, $pattern, [System.Text.RegularExpressions.RegexOptions]::Singleline)

$dict = @{}
foreach ($m in $matches) {
    $id = $m.Groups[1].Value
    $name = $m.Groups[2].Value
    $img = $m.Groups[3].Value
    if (![string]::IsNullOrWhiteSpace($img)) {
        if (-not $dict.ContainsKey($img)) {
            $dict[$img] = [System.Collections.Generic.List[string]]::new()
        }
        $dict[$img].Add("${id} - ${name}")
    }
}

Write-Host "Found $($dict.Keys.Count) unique image paths."
foreach ($k in $dict.Keys) {
    if ($dict[$k].Count -gt 1) {
        Write-Host "`nREPEATED IMAGE: $k"
        foreach ($item in $dict[$k]) {
            Write-Host "   -> $item"
        }
    }
}
