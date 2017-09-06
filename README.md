# PINPAD FERRET!

Plug and play tool to verify if an acquirer is supported by a specific pinpad, or a group or pinpads.

## Dependencies

Here's a brief description of all dependencies the project consumes.

Dependency name | Why I used it? | Where to find it?
--- | --- | ---
MicroPos SDK | To communicate with pinpad(s) attached to the machine | [Here](https://www.nuget.org/packages/MicroPos.Desktop.Pack/)
Castle Windsor | To add pinpad information to the IoC container | [Here](https://www.nuget.org/packages/Castle.Windsor/)
MarkdownLog | To show awesome tables on console | [Here](https://www.nuget.org/packages/MarkdownLog/)

## How to use

The Ferret currently support the following operations:

### Connect

Connects to one os multiple pinpads.

**Arguments**

Argument | Type | Description
--- | --- | ---
`all` | `bool` | Indicates whether the Ferret should connect to all pinpads attached to the machine, or just the first one found.

**Examples**

Command | Description
--- | ---
`$ ferret connect` | Connects to the first pinpad found on the machine
`$ ferret connect --all` | Connects to all pinpads found on the machine

### Scan

Scan all acquirers in the pinpad.

**Arguments**

Argument | Type | Description
--- | --- | ---
`all` | `bool` | Indicates whether the Ferret should scan all pinpads already connected, or just a specified one.
`port` | `string` | Is the port in which the pinpad to scan is attached to.
`ranges` | `int[]` | Specify the range of indexes to be searched for. Each index represents an acquirer.
`log` | `bool` | Show the scan progress.
`hasAcquirer` | `string` | Specify an acquirer to be searched for in the pinpad.

**Examples**

Command | Description
--- | ---
`$ ferret scan --all` | Scan all pinpads already connected. Does not show scan progress.
`$ ferret scan --port COM8` | Scan a specific pinpad - the pinpad must be already connected. Does not show scan progress.
`$ ferret scan --all --ranges 5 8 16` | Scan all pinpads already connected, but only searches for acquirers in the indexes 5, 8 and 16. Does not show log progress.
`$ ferret scan --port COM8 --ranges 5 8 16` | Scan a specific pinpad - the pinpad must be already connected, but only searches for acquirers in the indexes 5, 8 and 16. Does not show log progress.
`$ ferret scan --all --log` | Scan all pinpads already connected. Shows scan progress.
`$ ferret scan --all --hasAcquirer Stone` | Scan all pinpads already connected. Shows if the specified acquirer is supported by each pinpad.

### Pay

Executes a monetary transaction in the pinpad. The pinpad used in the transaction is the first found in the machine (or the only one connected to it).

:warning: The transaction, if approved, well always be cancelled.

**Arguments**

Argument | Type | Description
--- | --- | ---
`amount` | `double` | Transaction amount.
`itk` | `string` | Transaction identifier.
`showTransactions` | `bool` | Shows all transactions already approved.

**Examples**

Command | Description
--- | ---
`$ ferret pay --amount 12` | Initiates a transaction with the amount of R$ 10. A **`Guid`** will be set as transaction identifier.
`$ ferret pay --amount --itk MY_TRANSACTION_ID` | Initiates a transaction with the amount of R$ 10. The transaction identifier will be set as **MY_TRANSACTION_ID**.
`$ ferret pay --showTransactions` | Shows all transactions already approved by the application.

### Clean

Cleans the console.

**Example**

```
$ ferret clean
```

## Me

If you want to talk to me, use telegram (rohanaceres) or [mail me](mailto:ceresrohana+ferret@gmail.com).