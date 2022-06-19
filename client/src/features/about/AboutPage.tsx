import { Container, Typography, ButtonGroup, Button, Alert, AlertTitle, List, ListItem, ListItemText } from "@mui/material";
import { useState } from "react";
import agent from "../../app/api/agent";

export default function AboutPage() {

  const [validationErrors, setValidationErrors] = useState<string[]>([]);

  const getValidationErrors = async () => {
    try {
      await agent.TestErrors.getValidationError();
    } catch (e: any) {
      setValidationErrors(e);
    }
  };

  return (
    <Container>
      <Typography gutterBottom variant='h2'>Errors for testing purposes</Typography>
      <ButtonGroup fullWidth>
        <Button variant='contained' onClick={() => agent.TestErrors.get400Error().catch(error => console.log(error))}>Test 400 Error</Button>
        <Button variant='contained' onClick={() => agent.TestErrors.get401Error().catch(error => console.log(error))}>Test 401 Error</Button>
        <Button variant='contained' onClick={() => agent.TestErrors.get404Error().catch(error => console.log(error))}>Test 404 Error</Button>
        <Button variant='contained' onClick={() => agent.TestErrors.get500Error().catch(error => console.log(error))}>Test 500 Error</Button>
        <Button variant='contained' onClick={getValidationErrors}>Test Validation Error</Button>
      </ButtonGroup>
      {validationErrors.length > 0 &&
        <Alert severity="error">
          <AlertTitle> Validation errors </AlertTitle>
          <List>
            {validationErrors.map(e => (
              <ListItem key={e}>
                <ListItemText> {e} </ListItemText>
              </ListItem>))}
          </List>
        </Alert>
      }
    </Container>
  )
}