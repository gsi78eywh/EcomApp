$html = (Invoke-WebRequest -Uri "http://localhost:5023/menu" -UseBasicParsing).Content
$imgSrcs = [regex]::Matches($html, 'src="(/images/jmt/[^"]+)"') | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique

Write-Host "Found $($imgSrcs.Count) unique image URLs on /menu"
$failCount = 0
foreach ($src in $imgSrcs) {
    try {
        $url = "http://localhost:5023" + $src
        $r = Invoke-WebRequest -Uri $url -Method Head -UseBasicParsing
        if ($r.StatusCode -ne 200) {
            Write-Host "FAIL ($($r.StatusCode)): $src"
            $failCount++
        }
    } catch {
        Write-Host "ERROR: $src - $_"
        $failCount++
    }
}

if ($failCount -eq 0) {
    Write-Host "`nSUCCESS: All $($imgSrcs.Count)/$($imgSrcs.Count) images returned HTTP 200 OK!"
} else {
    Write-Host "`nWARNING: $failCount images failed!"
}
