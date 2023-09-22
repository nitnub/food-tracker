// cheat gfi PrimalItemResource_ApexDrop_Allo_C 100 50 0 | cheat gfi PrimalItemArtifact_11_C 100 50 0 | cheat gfi PrimalItemArtifact_08_C 100 50 0 | cheat gfi PrimalItemArtifact_06_C 100 50 0 | cheat gfi PrimalItemArtifact_09_C 100 50 0 | cheat gfi PrimalItemResource_ApexDrop_Basilo_C 100 50 0 | cheat gfi PrimalItemResource_ApexDrop_Giga_C 100 50 0 | cheat gfi PrimalItemResource_ApexDrop_Tuso_C 100 50 0 | cheat gfi PrimalItemResource_ApexDrop_Rex_C 100 50 0 | cheat gfi PrimalItemResource_ApexDrop_Yuty_C 100 50 0 |

const testResp = {
  userId: 202,
  resultCount: 3,
  reactions: [
    {
      // userId: 12,
      reactionId: 1,
      active: true,
      subsidedOn: 'DATE_STRING',
      modifiedOn: 'DATE_STRING',
      identifiedOn: 'DATE_STRING',
      deletedOn: 'DATE_STRING',
      food: {
        id: 'NEEDED?',
        reactionScope: 'Individual Food',
        name: 'Apple',
        vegetarian: true,
        vegan: true,
        glutenFree: true,
        fodMap: {
          id: 16,
          category: 'Fresh Fruit',
          name: 'Apple',
          freeUse: false,
          oligos: false,
          fructose: true,
          polyols: true,
          lactose: false,
          color: 'Red',
          maxIntake: '0g',
        },
      },
      reaction: { category: 'Stomach', type: 'Vomiting', severity: 'Severe' },
    },
  ],
};

// cheat gfi PrimalItemResource_ApexDrop_Allo 200 1 0 | cheat gfi PrimalItemArtifact_11 1 1 0 | cheat gfi PrimalItemArtifact_08 1 1 0 | cheat gfi PrimalItemArtifact_06 1 1 0 | cheat gfi PrimalItemArtifact_09 1 1 0 | cheat gfi PrimalItemResource_ApexDrop_Basilo 200 1 0 | cheat gfi PrimalItemResource_ApexDrop_Giga 200 1 0 | cheat gfi PrimalItemResource_ApexDrop_Tuso 200 1 0 | cheat gfi PrimalItemResource_ApexDrop_Yuty 200 1 0 |

"id": 13,
"userId": 202,
"active": false,
"subsidedOn": null,
"modifiedOn": "2023-04-14T02:28:26.945Z",
"identifiedOn": "2023-04-08T02:50:32.525Z",
"deletedOn": null,
"reactionType": "Vomiting",
"reactionCategory": "Stomach",
"reactionScope": "food",
"foodName": "Apple",
"vegetarian": true,
"vegan": true,
"glutenFree": true,
"reactionSeverity": "Severe",
"fodId": 16,
"fodCategory": "Fresh Fruit",
"fodName": "apple",
"fodFreeUse": false,
"fodOligos": false,
"fodFructose": true,
"fodPolyols": true,
"fodLactose": false,
"fodColor": "Red",
"maxIntake": "0g",
"maxIntakeTest": "0g"


const cow = {"command":"DO","rowCount":null,"oid":null,"rows":[],"fields":[],"_types":{"_types":{"arrayParser":{},"builtins":{"BOOL":16,"BYTEA":17,"CHAR":18,"INT8":20,"INT2":21,"INT4":23,"REGPROC":24,"TEXT":25,"OID":26,"TID":27,"XID":28,"CID":29,"JSON":114,"XML":142,"PG_NODE_TREE":194,"SMGR":210,"PATH":602,"POLYGON":604,"CIDR":650,"FLOAT4":700,"FLOAT8":701,"ABSTIME":702,"RELTIME":703,"TINTERVAL":704,"CIRCLE":718,"MACADDR8":774,"MONEY":790,"MACADDR":829,"INET":869,"ACLITEM":1033,"BPCHAR":1042,"VARCHAR":1043,"DATE":1082,"TIME":1083,"TIMESTAMP":1114,"TIMESTAMPTZ":1184,"INTERVAL":1186,"TIMETZ":1266,"BIT":1560,"VARBIT":1562,"NUMERIC":1700,"REFCURSOR":1790,"REGPROCEDURE":2202,"REGOPER":2203,"REGOPERATOR":2204,"REGCLASS":2205,"REGTYPE":2206,"UUID":2950,"TXID_SNAPSHOT":2970,"PG_LSN":3220,"PG_NDISTINCT":3361,"PG_DEPENDENCIES":3402,"TSVECTOR":3614,"TSQUERY":3615,"GTSVECTOR":3642,"REGCONFIG":3734,"REGDICTIONARY":3769,"JSONB":3802,"REGNAMESPACE":4089,"REGROLE":4096}},"text":{},"binary":{}},"RowCtor":null,"rowAsArray":true}