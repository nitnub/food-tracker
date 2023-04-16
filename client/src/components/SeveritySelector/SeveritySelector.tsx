import FormControl from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormLabel from '@mui/material/FormLabel';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import { MouseEvent, useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import { Severity } from '../../types/dbTypes';
import styles from './SeveritySelector.module.css';
// export default function Radio(severities: Severity[]) {
export default function SeveritySelector({
  reactionTypeId,
  severities,
  categoryId,
}: {
  reactionTypeId: number;
  categoryId: number;
  severities: Severity[];
}) {
  const { appContext, setAppContext } = useContext(AppContext);

  const handler = async (e: any) => {
    // if (e === null) return;
    const updatedReaction = {
      // userId: appContext.user?.id,
      ...JSON.parse(e.target.value),
      reactionTypeId,
      elementId: appContext.activeFood.id,
    };
    console.log(updatedReaction);
    // console.log(JSON.parse(e))
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(updatedReaction)
  };

    const resp = await fetch(`http://localhost:3200/api/v1/reaction/14`, requestOptions);
    const json = await resp.json();
    console.log(json)
  };

  return (
    <>
      <FormControl>
        {/* <FormLabel id="demo-row-radio-buttons-group-label">Gender</FormLabel> */}

        <RadioGroup
          onChange={handler}
          row
          aria-labelledby="demo-row-radio-buttons-group-label"
          name="row-radio-buttons-group"
        >
          {severities.map((severity: Severity, index: number) => {
            return (
              <FormControlLabel
                className={styles.radioLabel}
                value={JSON.stringify({
                  // category: categoryId,
                  severityId: severity.id,
                  // reactionTypeId: reactionTypeId,
                })}
                control={<Radio size="small" />}
                label={severity.name}
              />
            );
          })}

          {/* <FormControlLabel value="female" control={<Radio />} label="Female" />
          <FormControlLabel value="male" control={<Radio />} label="Male" />
          <FormControlLabel value="other" control={<Radio />} label="Other" /> */}
        </RadioGroup>
      </FormControl>
    </>
  );
  // return (
  //   <>
  //     {severities.map((severity: Severity, index: number) => {
  //       return (
  //         <>
  //           <div key={`i${index}`}>
  //             <input
  //               key={`i${index}`}
  //               type="radio"
  //               id={severity.id.toString()}
  //               name="severity"
  //               value={severity.name}
  //             />
  //             <label key={`${index}`} htmlFor={severity.name}>
  //               {severity.name}{' '}
  //             </label>
  //           </div>

  //           <FormControl>
  //             <FormLabel id="demo-row-radio-buttons-group-label">
  //               Gender
  //             </FormLabel>

  //             <RadioGroup
  //               row
  //               aria-labelledby="demo-row-radio-buttons-group-label"
  //               name="row-radio-buttons-group"
  //             >
  //               <FormControlLabel
  //                 value="female"
  //                 control={<Radio />}
  //                 label="Female"
  //               />
  //               <FormControlLabel
  //                 value="male"
  //                 control={<Radio />}
  //                 label="Male"
  //               />
  //               <FormControlLabel
  //                 value="other"
  //                 control={<Radio />}
  //                 label="Other"
  //               />

  //             </RadioGroup>

  //           </FormControl>
  //         </>
  //       );
  //     })}
  //   </>
  // );
}
