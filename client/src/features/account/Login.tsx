import { LoadingButton } from "@mui/lab";
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import { Container, Paper, Avatar, Typography, Box, TextField, Grid } from "@mui/material";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { FieldValues, useForm } from 'react-hook-form';
import { useAppDispatch } from "../../app/store/configureStore";
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { signInUser } from "./accountSlice";


const theme = createTheme();

export default function Login() {

    const navigate = useNavigate();
    const location = useLocation() as any;
    
    const dispatch = useAppDispatch();
    

    const { register, handleSubmit, formState: { isSubmitting, errors, isValid } } = useForm({
        mode: 'all'
    });

    // submit
    async function submitForm(data: FieldValues) {
        try {
            await dispatch(signInUser(data));
            navigate(location.state?.from?.pathname || '/catalog');
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <ThemeProvider theme={theme}>
        <Container component={Paper} maxWidth="sm" 
            sx={{display: 'flex', flexDirection: 'column', alignItems: 'center', p: 4}}>
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
            Sign in
            </Typography>
            <Box component="form" onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
            <TextField
                margin="normal"
                fullWidth
                label="Username"
                autoFocus
                {...register('username', {required: 'Username is required'})}
                error={!!errors.username}
                helperText={(errors.username as any)?.message}
            />
            <TextField
                margin="normal"
                fullWidth
                label="Password"
                type="password"
                {...register('password', {required: 'Password is required'})}
                error={!!errors.password}
                helperText={(errors.password as any)?.message}
            />
            <LoadingButton
                loading={isSubmitting}
                disabled={!isValid}
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
            >
                Sign In
            </LoadingButton>
            <Grid container>
                <Grid item>
                <Link to="/register">
                    {"Don't have an account? Sign Up"}
                </Link>
                </Grid>
            </Grid>
            </Box>
        </Container>
    </ThemeProvider>
    )
}