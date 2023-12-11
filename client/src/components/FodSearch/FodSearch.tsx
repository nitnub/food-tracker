import React, { useEffect } from 'react';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import Stack from '@mui/material/Stack';
import FodMapAPI from '../../utils/FodMapAPI';
import { useState } from 'react';
import FodCard from '../FodCard';
import { FodOptionType } from '../FodCard/FodCard';

export default function FodSearch({
  setFoodForm,
  foodForm,
}: {
  setFoodForm: Function;
  foodForm: object;
}) {
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

  return (
    <>
      <Stack spacing={1} sx={{ width: 300 }}>
        <Autocomplete
          {...defaultProps}
          id="fod-search"
          value={value}
          onChange={(event: any, newValue: FodOptionType | null) => {
            setValue(newValue);
            setFoodForm(() => ({
              ...foodForm,
              fodmapId: newValue === null ? null : newValue.id,
            }));
          }}
          renderInput={(params) => (
            <TextField {...params} label="FODMAP Name" variant="standard" />
          )}
        />
      </Stack>

      {value && <FodCard item={value} />}
    </>
  );
}
