import * as React from 'react';
import Box from '@mui/material/Box';
import styles from './FodCard.module.css';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';

export interface FodOptionType {
  aliasList: string[];
  aliasPrimary: string;
  category: string;
  color: string;
  freeUse: boolean;
  fructose: boolean;
  id: number;
  lactose: boolean;
  maxIntake: string;
  name: string;
  oligos: boolean;
  polyols: boolean;
}

// const bull = (
//   <Box
//     component="span"
//     sx={{ display: 'inline-block', mx: '2px', transform: 'scale(0.8)' }}
//   >
//     •
//   </Box>
// );

export default function FodCard({ item }: { item: FodOptionType }) {
  const aliasFormatted = (
    <div className={styles.aliasHolder}>
      {item.aliasList.map((el, index: number) => {
        return (
          <div key={index} className={styles.aliasItem}>
            {el}
          </div>
        );
      })}
    </div>
  );

  const maxIntakeLabel = <div className={styles.maxIntake}>{item.maxIntake?.toString()}<div>Limit</div></div>;

  // const displayLabel ()
  const activeLabel = (label: string) => {
    // return <Tooltip title={`Contains ${label.toLowerCase()}`} followCursor><div>{label}</div></Tooltip>
    return (
      <Tooltip title={`Contains ${label}`} followCursor>
        <div>{label}</div>
      </Tooltip>
    );
  };

  const inactiveLabel = (label: string) => {
    return <div className={styles.notPresent}>{label}</div>;
  };

  type DynamicLabel = 'oligos' | 'fructose' | 'lactose' | 'polyols';
  const dynamicOogLabel = (str: DynamicLabel, fodItem: FodOptionType) => {
    return item[str] === true ? activeLabel(str) : inactiveLabel(str);
  };

  const oogLabel = (
    <div>
      {['oligos', 'fructose', 'polyols', 'lactose'].map((el) =>
        dynamicOogLabel(el as DynamicLabel, item)
      )}
    </div>
  );
  console.log('item:', item);
  return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
        </Typography>
        <Typography variant="h5" component="div">
          {item.name}
        </Typography>
          {item.category}
        {/* <Typography sx={{ mb: 1.5 }} color="text.secondary"> */}
        {/* <Typography > */}
        {aliasFormatted}
        {/* </Typography> */}
        <div className={styles.cardBody}>
          {/* <Typography variant="body2"> */}

          {/* {item.maxIntake ? item.maxIntake.toString() : ''} */}
          {item.maxIntake ? maxIntakeLabel : ''}
        <div className={styles.fodColorCircle} />
        {/* </Typography> */}
        <Typography variant="body2">
          <br />
          {oogLabel}
          {'"a benevolent smile"'}
        </Typography>
          </div>
        {/* <Tooltip title="Contains oligos">{item.name} </Tooltip> */}
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
    </Card>
  );
}
