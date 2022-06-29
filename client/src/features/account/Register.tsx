import { LoadingButton } from "@mui/lab";
import { Container, Paper, Avatar, Typography, Box, TextField, Grid } from "@mui/material";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import agent from "../../app/api/agent";
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';


export default function Register() {
    //const history = useHistory();
    const { register, handleSubmit, setError, formState: { isSubmitting, errors, isValid } } = useForm({
        mode: 'all'
    });

    function handleApiErrors(errors: any) {
        if (errors) {
            errors.forEach((error: string) => {
                if (error.includes('Password')) {
                    setError('password', { message: error })
                } else if (error.includes('Email')) {
                    setError('email', { message: error })
                } else if (error.includes('Username')) {
                    setError('username', { message: error })
                } else if (error.includes('firstName')) {
                    setError('firstName', { message: error })
                } else if (error.includes('lastName')) {
                    setError('lastName', { message: error })
                }
            });
        }
    }

    return (
        <Container component={Paper} maxWidth="sm" sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', p: 4 }}>
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Register
            </Typography>
            <Box component="form"
                onSubmit={handleSubmit((data) =>
                    agent.Account.register(data)
                        .then(() => {
                            toast.success('Registration successful - you can now login');
                            window.location.href = '/login';
                        })
                        .catch(error => handleApiErrors(error))
                )}
                noValidate sx={{ mt: 1 }}
            >
                <TextField
                    margin="normal"
                    fullWidth
                    label="Firstname"
                    autoFocus
                    {...register('firstName', { required: 'Ad alanı gereklidir!' })}
                    error={!!errors.firstName}
                    helperText={(errors.firstName as any)?.message}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Lastname"
                    autoFocus
                    {...register('lastName', { required: 'Soyadı alanı gereklidir!' })}
                    error={!!errors.lastName}
                    helperText={(errors.lastName as any)?.message}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Username"
                    autoFocus
                    {...register('username', { required: 'Kullanıcı adı alanı gereklidir!' })}
                    error={!!errors.username}
                    helperText={(errors.username as any)?.message}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Email address"
                    {...register('email', {
                        required: 'Email alanı gereklidir!',
                        pattern: {
                            value: /^\w+[\w-.]*@\w+((-\w+)|(\w*)).[a-z]{2,3}$/,
                            message: 'Not a valid email address'
                        }
                    })}
                    error={!!errors.email}
                    helperText={(errors.email as any)?.message}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Password"
                    type="password"
                    {...register('password', {
                        required: 'Parola alanı gereklidir!',
                        pattern: {
                            value: /(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$/,
                            message: 'Password is not complex enough'
                        }
                    })}
                    error={!!errors.password}
                    helperText={(errors.password as any)?.message}
                />
                <LoadingButton
                    disabled={!isValid}
                    loading={isSubmitting}
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Register
                </LoadingButton>
                <Grid container>
                    <Grid item>
                        <Link to='/login'>
                            {"Already have an account? Sign In"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    );
}