Start-Sleep -Seconds 3

Write-Host "Verifying http://localhost:5023/menu..."
$resp = Invoke-WebRequest -Uri "http://localhost:5023/menu" -UseBasicParsing
Write-Host "Status code: $($resp.StatusCode)"
$content = $resp.Content

# 1. Check Interactive Features
$hasCravingFinder = $content.Contains('craving-finder-card')
$hasLiveStatus = $content.Contains('live-status-pill')
$hasDeliveryEstimator = $content.Contains('delivery-calculator-widget')
$hasFavorites = $content.Contains('card-fav-btn')
$hasFloatingTray = $content.Contains('floating-tray-bar')
$hasTrayDrawer = $content.Contains('jmt-tray-drawer')
$hasToast = $content.Contains('jmt-toast-container')

Write-Host "`n--- Interactive Features Check ---"
Write-Host "1. Craving & Mood Finder present: $hasCravingFinder"
Write-Host "2. Live Status & Weather Beacon present: $hasLiveStatus"
Write-Host "3. Dalaguete Delivery Estimator present: $hasDeliveryEstimator"
Write-Host "4. Dish Favorites (Heart Wishlist) present: $hasFavorites"
Write-Host "5. Floating Order Tray present: $hasFloatingTray"
Write-Host "6. Slide-in Tray Drawer present: $hasTrayDrawer"
Write-Host "7. Toast Notification System present: $hasToast"

# 2. Check Uniform Card Sizing Classes
$hasUniformMedia = $content.Contains('jmt-card-media-box')
$hasUniformTitle = $content.Contains('jmt-card-title')
$hasUniformDesc = $content.Contains('jmt-card-desc')
$hasUniformFooter = $content.Contains('jmt-card-footer-action')

Write-Host "`n--- Uniform Card Sizing Check ---"
Write-Host "Uniform 4:3 Media Box present: $hasUniformMedia"
Write-Host "Uniform 2-line Title present: $hasUniformTitle"
Write-Host "Uniform 2-line Desc present: $hasUniformDesc"
Write-Host "Uniform Footer Action present: $hasUniformFooter"

# 3. Check All Images on /menu Return HTTP 200 OK
Write-Host "`n--- Image HTTP 200 Verification ---"
$imgSrcs = [regex]::Matches($content, 'src="(/images/jmt/[^"]+)"') | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique
Write-Host "Total unique image URLs on /menu: $($imgSrcs.Count)"
$failCount = 0
foreach ($src in $imgSrcs) {
    try {
        $r = Invoke-WebRequest -Uri ("http://localhost:5023" + $src) -Method Head -UseBasicParsing
        if ($r.StatusCode -ne 200) {
            Write-Host "FAIL: $src"
            $failCount++
        }
    } catch {
        Write-Host "ERROR: $src"
        $failCount++
    }
}
Write-Host "Image HTTP Test: $(if ($failCount -eq 0) { 'All passed 200 OK' } else { "$failCount failed" })"

# 4. Check for any AI image references
$hasAi = $content.Contains('jmt_footlong_cheese.png') -or $content.Contains('jmt_pancake_syrup.png') -or $content.Contains('jmt_local_bread.png') -or $content.Contains('jmt_ube_cake.png') -or $content.Contains('jmt_ice_cold_beers.png')
Write-Host "Any AI generated image found: $hasAi"

Write-Host "`nVerification complete!"
