Start-Sleep -Seconds 3

Write-Host "Verifying http://localhost:5023/menu..."
$resp = Invoke-WebRequest -Uri "http://localhost:5023/menu" -UseBasicParsing
Write-Host "Status code: $($resp.StatusCode)"

$content = $resp.Content

# Check Container 1: Facebook Compiled Product Photos Feed
$hasFbContainer = $content.Contains('id="facebookCompiledFeedContainer"')
Write-Host "Container 1 (Facebook Compiled Feed) present: $hasFbContainer"

$hasFbGrid = $content.Contains('id="fbPhotosGrid"')
Write-Host "Facebook Photos Grid present: $hasFbGrid"

$hasFbFilterPills = $content.Contains('fb-filter-pill')
Write-Host "Facebook Filter Pills present: $hasFbFilterPills"

$hasFbLikeBtn = $content.Contains('toggleFbPostLike')
Write-Host "Interactive FB Like button present: $hasFbLikeBtn"

# Check Container 2: Food Categorization System
$hasCatContainer = $content.Contains('id="foodCategorizationContainer"')
Write-Host "Container 2 (Food Categorization System) present: $hasCatContainer"

$hasCatNavCards = $content.Contains('food-cat-nav-card')
Write-Host "Food Category Navigation Cards present: $hasCatNavCards"

$hasCombosCategory = $content.Contains('Value Feast Combos')
Write-Host "Value Feast Combos category present: $hasCombosCategory"

# Count occurrences of fb-post-item-card
$fbCardMatches = [regex]::Matches($content, 'class="fb-post-item-card')
Write-Host "Facebook Compiled Product Cards rendered: $($fbCardMatches.Count)"

# Check wall posters section
$hasWallPosters = $content.Contains('id="wallPostersSection"')
Write-Host "Wall Posters section present: $hasWallPosters"

# Check that NO AI image paths are referenced
$hasAi1 = $content.Contains('jmt_footlong_cheese.png')
$hasAi2 = $content.Contains('jmt_pancake_syrup.png')
$hasAi3 = $content.Contains('jmt_local_bread.png')
$hasAi4 = $content.Contains('jmt_ube_cake.png')
$hasAi5 = $content.Contains('jmt_ice_cold_beers.png')
$anyAi = $hasAi1 -or $hasAi2 -or $hasAi3 -or $hasAi4 -or $hasAi5
Write-Host "Any AI generated image found in HTML: $anyAi"

Write-Host "`nAll container checks complete!"
