Connect-AzAccount

#Variables 
$location       = "UK South"
$resourceGroupName   = "rg-uksouth-UnityWebBaseProjectCICDAzure"
$vmName         = "vm-uksouth-build-unitywebbase"

# Create VNet 
$virtualNetwork = New-AzVirtualNetwork  -ResourceGroupName $resourceGroupName `
                                        -Name "vnet-$vmName" `
                                        -Location $location `
                                        -AddressPrefix 10.0.0.0/16

# Create Subnet
$subnetConfig = Add-AzVirtualNetworkSubnetConfig    -Name "default" `
                                                    -VirtualNetwork $virtualNetwork `
                                                    -AddressPrefix '10.0.0.0/24'
# Add Subnet
$virtualNetwork | Set-AzVirtualNetwork

# Create VM 
New-AzVM    -ResourceGroupName $resourceGroupName  `
            -Name $vmName `
            -Location $location `
            -VirtualNetworkName "myVnet" `
            -SubnetName "mySubnet" `
            -SecurityGroupName "myNetworkSecurityGroup" `
            -PublicIpAddressName "myPublicIpAddress" `

# Add extensions
