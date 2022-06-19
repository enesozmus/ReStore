import { Product } from "../../app/models/product";
import * as React from 'react';
import { styled } from '@mui/material/styles';
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import CardActions from '@mui/material/CardActions';
import Collapse from '@mui/material/Collapse';
import Avatar from '@mui/material/Avatar';
import IconButton, { IconButtonProps } from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import { purple } from '@mui/material/colors';
import FavoriteIcon from '@mui/icons-material/Favorite';
import ShareIcon from '@mui/icons-material/Share';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { Button } from "@mui/material";
import { Link } from "react-router-dom";
import agent from "../../app/api/agent";
import { LoadingButton } from "@mui/lab";
import { useStoreContext } from "../../app/context/ReStoreContext";

interface Props {
  product: Product;
}

interface ExpandMoreProps extends IconButtonProps {
  expand: boolean;
}

const ExpandMore = styled((props: ExpandMoreProps) => {
  const { expand, ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
  marginLeft: 'auto',
  transition: theme.transitions.create('transform', {
    duration: theme.transitions.duration.shortest,
  }),
}));

export default function ProductCard({ product }: Props) {

  // Basket
  const [loading, setLoading] = React.useState(false);
  // badgeContent
  const {setBasket} = useStoreContext();

  function handleAddItem(productId: number) {
    setLoading(true);
    agent.Basket.addItem(productId)
      .then(basket => setBasket(basket))
      .catch(error => console.log(error))
      .finally(() => setLoading(false));
  }
  // -- Basket

  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };



  return (
    <Card sx={{ height: '100%' }}>
      <CardHeader
        avatar={
          <Avatar sx={{ bgcolor: purple[500] }} aria-label="recipe">
            {product.name.charAt(0).toUpperCase()}
          </Avatar>
        }
        action={
          <IconButton aria-label="settings">
            <MoreVertIcon />
          </IconButton>
        }
        title={product.name}
        titleTypographyProps={{
          sx: { fontWeight: 'bold', color: '#e91e63' }
        }}
        subheader="September 14, 2016"
      />
      <CardMedia
        component="img"
        sx={{ height: 194, objectFit: 'contain', bgcolor: 'primary.light' }}
        image={product.pictureUrl}
        alt="Paella dish"
      />
      <CardContent>
        <Typography gutterBottom color="secondary" variant="h5">
          ₺{(product.price / 100).toFixed(2)}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {product.brand} /
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {product.description.substring(0, 120)}...
        </Typography>
        <CardActions>

          <LoadingButton
            loading={loading}
            onClick={() => handleAddItem(product.id)}
            size="small">
            Sepete Ekle
          </LoadingButton>

          <Button
            component={Link}
            to={`/catalog/${product.id}`}
            size="small">
            View
          </Button>

        </CardActions>

      </CardContent>
      <CardActions disableSpacing>
        <IconButton aria-label="add to favorites">
          <FavoriteIcon />
        </IconButton>
        <IconButton aria-label="share">
          <ShareIcon />
        </IconButton>
        <ExpandMore
          expand={expanded}
          onClick={handleExpandClick}
          aria-expanded={expanded}
          aria-label="show more"
        >
          <ExpandMoreIcon />
        </ExpandMore>
      </CardActions>
      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>

          <Typography paragraph>Method:</Typography>
          <Typography paragraph>
            {product.description}
          </Typography>
          <Typography paragraph>
            Lorem ipsum dolor, sit amet consectetur adipisicing elit.
          </Typography>
          <Typography paragraph>
            Lorem ipsum dolor, sit amet consectetur adipisicing elit.
          </Typography>
          <Typography>
            Set aside off of the heat to let rest for 10 minutes, and then serve.
          </Typography>
        </CardContent>
      </Collapse>
    </Card>
  )
}