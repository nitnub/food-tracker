import styles from './FodCard.module.css';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';
import CheckIcon from '@mui/icons-material/Check';

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


export default function FodCard({ item }: { item: FodOptionType }) {
  const aliasFormatted = (
    <div className={styles.aliasHolder}>
      <div className={styles.aliasLabel}>Aliases: &nbsp; </div>
      {item.aliasList.map((el, index: number) => {
        return (
          <span key={index} className={styles.aliasItem}>
            {el}
          </span>
        );
      })}
    </div>
  );

  const fodColor = (color: string | null) => {
    let outputColor = 'white';
    if (color === null) {
      return outputColor;
    }
    if (color.toLowerCase() === 'green') outputColor = '#258f45';
    if (color.toLowerCase() === 'yellow') outputColor = '#d6a211';
    if (color.toLowerCase() === 'red') outputColor = '#8f252a';
    return outputColor;
  };

  // const maxIntakeLabel = <div style={{backgroundColor: fodColor(item.color)}} className={styles.maxIntakeContainer}><div className={styles.maxIntake}>{item.maxIntake?.toString()}</div><div className={styles.maxIntakeSubheader}>Limit</div></div>;
  const maxIntakeLabel = (
    <div className={styles.maxIntakeContainer}>
      <div className={styles.maxIntake}>{item.maxIntake?.toString()}</div>
      <div className={styles.maxIntakeSubheader}>Max Daily Intake</div>
    </div>
  );

  const fodSafeLabel = (
    <div className={styles.fodSafeContainer}>
      <div className={styles.fodSafeCheckContainer}>
        <CheckIcon className={styles.fodSafeCheck} />
      </div>
      <div className={styles.fodSafeSubheader}>FODMAP Safe!</div>
    </div>
  );


  const activeLabel = (label: string) => {
    return (
      <Tooltip title={`Contains ${label}`} arrow>
        <div className={styles.activeReactant}>
          <ErrorOutlineIcon />
          <div>{label}</div>
        </div>
      </Tooltip>
    );
  };

  const inactiveLabel = (label: string) => {
    return (
      <Tooltip title={`free of ${label}`} arrow>
        <div className={styles.inAactiveReactant}>
          <CheckIcon />
          <div className={styles.notPresent}>{label}</div>
        </div>
      </Tooltip>
    );
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
    <Card
      className={styles.card}
    >
      <div
        className={styles.fodFlag}
        style={{ backgroundColor: fodColor(item.color) }}
      />
      <CardContent>
        <Typography
          sx={{ fontSize: 14 }}
          color="text.secondary"
          gutterBottom
        ></Typography>
        <Typography variant="h5" component="div">
          <div className={styles.itemNameContainer}>{item.name}</div>
        </Typography>
        {item.category}
        {/* <Typography sx={{ mb: 1.5 }} color="text.secondary"> */}
        {/* <Typography > */}
        {aliasFormatted}
        {/* </Typography> */}
        <div className={styles.cardBody}>
          {/* <Typography variant="body2"> */}

          {/* {item.maxIntake ? item.maxIntake.toString() : ''} */}
          <div
            style={{ backgroundColor: fodColor(item.color) }}
            className={styles.fodColorCircle}
          />
          {item.maxIntake ? maxIntakeLabel : fodSafeLabel}
          {/* </Typography> */}
          <Typography variant="body2">
            <br />
            {oogLabel}

          </Typography>
        </div>

      </CardContent>
      {/* <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions> */}
    </Card>
  );
}