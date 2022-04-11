
# SportsAPI

A small API for creating teams and players. This project was built with .NET 6, EF Core and OData.

OData query information can be found here: [OData query data](https://www.odata.org/getting-started/basic-tutorial/#queryData)


## API Reference

All

### Players

#### Get all players

```http
  GET /v1/Players
```

| Query     | Type     | Example                                 | Description |
| :-------- | :------- | :-------------------------              | :--------   |
| `$orderby` | `string` | /v1/Players?$orderby=FirstName desc    | Returns sorted results by field name and a direction. |
| `$select`  | `string` | /v1/Players?$select=FirstName,LastName | Returns results with only selected fields. |
| `$expand`  | `string` | /v1/Players?$expand=Team | Returns results with each players 'Team' record populated. |

#### Get player

```http
  GET /v1/Players/{id}
```
| Parameter | Type     | Description  |
| :-------- | :------- | :----------- |
| `id`      | `int` | **Required**. Id of player to fetch |

| Query     | Type     | Example                                 | Description |
| :-------- | :------- | :-------------------------              | :--------   |
| `$select`  | `string` | /v1/Players/1?$select=FirstName,LastName | Returns result with only selected fields. |
| `$expand`  | `string` | /v1/Players/1?$expand=Team | Returns result with the 'Team' record populated. |

#### Create player

```http
  POST /v1/Teams
```
#### Accepts JSON
| Field | Type     | Description  |
| :-------- | :------- | :----------- |
| `FirstName`      | `string` | **Required**. First name of player |
| `LastName`  | `string` | **Required**. Last name of player |

### Teams

#### Get teams

```http
  GET /v1/Teams
```
| Query     | Type     | Example                                 | Description |
| :-------- | :------- | :-------------------------              | :--------   |
| `$orderby` | `string` | /v1/Players?$orderby=Name desc    | Returns sorted results by field name and a direction. |
| `$select`  | `string` | /v1/Players?$select=Name,Location | Returns results with only selected fields. |
| `$expand`  | `string` | /v1/Players?$expand=Players | Returns results with each teams 'Players' record populated. |

#### Get team

```http
  GET /v1/Teams/{id}
```
| Parameter | Type     | Description  |
| :-------- | :------- | :----------- |
| `id`      | `int` | **Required**. Id of team to fetch |

| Query     | Type      | Example                                 | Description |
| :-------- | :-------  | :-------------------------              | :--------   |
| `$select`  | `string` | /v1/Teams/1?$select=Name,Location | Returns result with only selected fields. |
| `$expand`  | `string` | /v1/Teams/1?$expand=Players | Returns result with the 'Players' records populated. |

#### Create team

```http
  POST /v1/Teams
```
#### Accepts JSON
| Field | Type     | Description  |
| :-------- | :------- | :----------- |
| `Name`      | `string` | **Required**. Name of team |
| `Location`  | `string` | **Required**. Location of team |

#### Assign player to team

```http
  POST /v1/Teams/{teamId}/Assign/{playerId}
```
| Parameter | Type      | Description  |
| :-------- | :-------  | :----------- |
| `teamId`      | `int` | **Required**. Id of team to assign to |
| `playerId`    | `int` | **Required**. Id of player to be assigned |

#### Unassign player from team

```http
  POST /v1/Teams/{teamId}/Unassign/{playerId}
```
| Parameter | Type      | Description  |
| :-------- | :-------  | :----------- |
| `teamId`      | `int` | **Required**. Id of team to unassign from |
| `playerId`    | `int` | **Required**. Id of player to be unassigned |
