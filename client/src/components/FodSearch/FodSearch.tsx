import React, { useEffect } from 'react';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import Stack from '@mui/material/Stack';
import FodMapAPI from '../../utils/FodMapAPI';
import { useState } from 'react';
import styles from './FodSearch.module.css';
import FodCard from '../FodCard';
import { FodOptionType } from '../FodCard/FodCard';

export default function FodSearch() {
  const [value, setValue] = React.useState<FodOptionType | null>(null);
  const [fodList, setFodList] = useState([]);
  const defaultProps = {
    options: fodList,
    getOptionLabel: (option: FodOptionType) => option.name,
  };

  useEffect(() => {
    const fodAPI = new FodMapAPI();
    const getFodList = async () => {
      const lst = await fodAPI.getAll();
      if (lst.status === 'success') {
        console.log(lst.data);
        setFodList(lst.data);
      }
    };
    getFodList();
  }, []);

  const fodColorEx = (colorString: string) => {
    if (colorString.toLowerCase() === 'green') return '#258f45';
    if (colorString.toLowerCase() === 'yellow') return '#d6a211';
    if (colorString.toLowerCase() === 'red') return '#8f252a';
    return 'black';
  };

  const fodColor = (color: string | null) => {
    let outputColor = 'black';
    if (color === null) {
      return styles.unknownFodColor;
    }
    if (color.toLowerCase() === 'green') outputColor = '#258f45';
    if (color.toLowerCase() === 'yellow') outputColor = '#d6a211';
    if (color.toLowerCase() === 'red') outputColor = '#8f252a';
    return styles[color.toLowerCase()];
    return 'black';
  };

  return (
    <>
      <Stack spacing={1} sx={{ width: 300 }}>
        <Autocomplete
          {...defaultProps}
          id="controlled-demo"
          value={value}
          onChange={(event: any, newValue: FodOptionType | null) => {
            setValue(newValue);
          }}
          renderInput={(params) => (
            <TextField {...params} label="controlled" variant="standard" />
          )}
        />
      </Stack>

      <div>{value?.aliasPrimary}</div>
      <div>Aliases:</div>
      <div>{value?.category}</div>
      <div>{value?.color}</div>
      <div>Free Use:</div>
      <div>{value?.freeUse}</div>
      <div>{value?.id}</div>
      {value && <FodCard item={value} />}
    </>
  );
}
