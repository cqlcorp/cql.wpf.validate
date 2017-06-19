# cql.wpf.validate
Straight forward and highly configurable validation for WPF application.

## Examples

### Configure views
```
{
    "viewConfigs": [
        {
            "name": "Login",
            "fields": [
                {
                    "name": "Username",
                    "isRequired": true,
                    "maxLength": 50
                },
                {
                    "name": "Password",
                    "isRequired": true
                }
            ]
        }
    ]
}
```

### Load config when view loads (in ViewModel)
`ViewConfig = await ValidationContext.LoadViewConfigAsync("Login")`

### Add custom validators
```
ViewConfig.AddCustomValidator(
    fieldName: "Password",
    validate:  o => _membershipService.Login(Username, Password),
    messageKey: "InvalidUsernamePassword");
```

### Bind config to view
```
ui:ViewValidatorControl ViewConfig="{Binding ViewConfig}"/>`
```

### Perform validation (in ViewModel)
```
var result = validationContext.Validate(ViewConfig, this);
if (result.IsValid)
{
    //...
}
```

### Display validation messages however you want
```
<ItemsControl ItemsSource="{Binding ValidationMessages}">
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <DockPanel>
                <Image Height="12" Source="{StaticResource Icon:Error}"/>
                <TextBlock Text="{Binding Message}" Foreground="Red"/>
            </DockPanel>    
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

### Use ResourceDictionary(s) for translations and/or custom messages...
```
<sys:String x:Key="InvalidUsernamePassword">Username is incorrect or the password does not match!</sys:String>
<sys:String x:Key="InvalidEmail">That does not appear to be a real email address!</sys:String>
```

### ...or provide your own message implementation
```
public class MyAppMessageProvider : IMessageProvider
{
    //...
}
```

### Configure your views using the default Json provider...
```
//no code needed!
```

### ...or provide your own config implementation
```
 public class MyAppConfigProvider : IConfigProvider
 {
     //...
 }
```

### Handle validation messages globally...
```
ValidationContext.Global.ValidationFailCommand = new HandleValidationFailCommand(); //Custom command
```

### ...or locally
```
var result = ValidationContext.Validate(ViewConfig, this);
if (!result.IsValid)
{
    ValidationMessages.AddRange(result.Messages);
}
```

## Built-in Validators

* Required field
* Number range
* Date range
* String length (min and max)
* Regex
* Custom - everything else you need
