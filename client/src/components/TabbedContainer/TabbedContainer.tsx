import * as React from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import FoodPicker from '../FoodPicker';
import ReactionDashboard from '../ReactionDashboard';
import UpdateContainer from '../UpdateContainer';
import FodMapPicker from '../FodMapPicker';
import MealModal from '../MealModal';
interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`vertical-tabpanel-${index}`}
      aria-labelledby={`vertical-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

function a11yProps(index: number) {
  return {
    id: `vertical-tab-${index}`,
    'aria-controls': `vertical-tabpanel-${index}`,
  };
}

export default function TabbedContainer() {
  const [value, setValue] = React.useState(0);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  return (
    <Box
      sx={{
        flexGrow: 1,
        bgcolor: 'background.paper',
        display: 'flex',
        height: 500,
      }}
    >
      <Tabs
        orientation="vertical"
        variant="scrollable"
        value={value}
        onChange={handleChange}
        aria-label="Vertical tabs example"
        sx={{ borderRight: 1, borderColor: 'divider' }}
      >
        <Tab label="Shop" {...a11yProps(0)} />
        <Tab label="Reactions" {...a11yProps(1)} />
        <Tab label="Add Food" {...a11yProps(2)} />
        <Tab label="FODMAP" {...a11yProps(3)} />
        <Tab label="Item Five" {...a11yProps(4)} />
        <Tab label="Item Six" {...a11yProps(5)} />
        <Tab label="MealModal Rough" {...a11yProps(6)} />
      </Tabs>
      <TabPanel value={value} index={0}>
        Shop!
      </TabPanel>
      <TabPanel value={value} index={1}>
        <FoodPicker />
        <ReactionDashboard />
      </TabPanel>
      <TabPanel value={value} index={2}>
        <UpdateContainer />
      </TabPanel>
      <TabPanel value={value} index={3}>
        <FodMapPicker foodForm={{}} setFoodForm={() => null} />
      </TabPanel>
      <TabPanel value={value} index={4}>
        Item Five
      </TabPanel>
      <TabPanel value={value} index={5}>
        Item Six
      </TabPanel>
      <TabPanel value={value} index={6}>
        <MealModal />
      </TabPanel>
    </Box>
  );
}
