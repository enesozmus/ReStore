import { Add, Delete, Remove } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import { Box, Button, Grid, Link, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { currencyFormat } from "../../app/util/util";
import { removeBasketItemAsync, addBasketItemAsync } from "./basketSlice";
import BasketSummary from "./BasketSummary";

export default function BasketPage() {

    const { basket, status } = useAppSelector(state => state.basket);
    const dispatch = useAppDispatch();


    if (!basket) return <Typography variant='h3'>Sepetiniz henüz boş!</Typography>


    return (
        <>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Ürün</TableCell>
                            <TableCell align="right">Fiyat</TableCell>
                            <TableCell align="center">Adet</TableCell>
                            <TableCell align="right">Toplam fiyat</TableCell>
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {basket.items.map((item) => (
                            <TableRow
                                key={item.productId}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <Box display='flex' alignItems='center'>
                                    <img src={item.pictureUrl} alt={item.name} style={{ height: 50, marginLeft: 20, marginTop: 10 }} />
                                    <span>{item.name}</span>

                                </Box>
                                <TableCell align="right">{currencyFormat(item.price)}</TableCell>
                                <TableCell align="center">

                                    <LoadingButton
                                        loading={status === 'pendingRemoveItem' + item.productId + 'rem'}
                                        onClick={() => dispatch(removeBasketItemAsync({ productId: item.productId, quantity: 1, name: 'rem' }))}
                                        color='error'
                                    >
                                        <Remove />
                                    </LoadingButton>

                                    {item.quantity}

                                    <LoadingButton
                                        loading={status === 'pendingAddItem' + item.productId}
                                        onClick={() => dispatch(addBasketItemAsync({ productId: item.productId }))}
                                        color='secondary'
                                    >
                                        <Add />
                                    </LoadingButton>

                                </TableCell>

                                <TableCell align="right">{currencyFormat(item.price * item.quantity)}</TableCell>
                                <TableCell align="right">
                                    <LoadingButton
                                        loading={status === 'pendingRemoveItem' + item.productId + 'del'}
                                        onClick={() => dispatch(removeBasketItemAsync({ productId: item.productId, quantity: item.quantity, name: 'del' }))}
                                        color='error'
                                    >
                                        <Delete />
                                    </LoadingButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Grid container>
                <Grid item xs={6} />
                <Grid item xs={6}>
                    <BasketSummary />
                    <Link href="/checkout">
                        <Button
                            fullWidth
                            size='large'
                            variant='contained'
                        >
                            Checkout
                        </Button>
                    </Link>
                </Grid>
            </Grid>
        </>
    )
}